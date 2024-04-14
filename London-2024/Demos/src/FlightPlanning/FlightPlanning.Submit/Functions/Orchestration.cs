namespace TaleLearnCode.FlightTracker.SubmitFlightPlan.Functions;

public class Orchestration(ILoggerFactory loggerFactory, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<Orchestration>();
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	private const string _externalEventName = "Approval";

	[Function("SubmitFlightPlan")]
	public async Task<HttpResponseData> SubmitFlightPlanAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData request,
		[DurableClient] DurableTaskClient durableTaskClient)
	{

		FlightPlanMessage flightPlan;
		try
		{

			flightPlan = await request.GetRequestParametersAsync<FlightPlanMessage>(_jsonSerializerOptions);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error deserializing request body.");
			return request.CreateBadRequestResponse(ex);
		}

		string formattedDepartureTime = flightPlan.DepartureTime.ToString("yyyy-MM-dd");
		_logger.LogInformation("Flight Plan Received: {Airline}-{FlightNumber} from {OriginAirport} to {DestinationAirport} on {DepartureTime:yyyy-MM-dd}.", flightPlan.Airline, flightPlan.FlightNumber, flightPlan.OriginAirport, flightPlan.DestinationAirport, formattedDepartureTime);

		SubmitFlightPlanDetails submitFlightPlanDetails = new()
		{
			FunctionHostAddress = request.Url!.AbsoluteUri.Replace(request.Url!.LocalPath, ""),
			FlightPlan = flightPlan
		};
		string instanceId = await durableTaskClient.ScheduleNewOrchestrationInstanceAsync("Orchestrator", submitFlightPlanDetails);
		_logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

		return durableTaskClient.CreateCheckStatusResponse(request, instanceId);

	}

	[Function("ApproveFlightPlan")]
	public static async Task<HttpResponseData> ApproveFlightPlanAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "flight-plan/approval/{instanceId}/{flightPlanApproved}")] HttpRequestData request,
		[DurableClient] DurableTaskClient durableTaskClient,
		string instanceId,
		bool flightPlanApproved)
	{
		await durableTaskClient.RaiseEventAsync(instanceId, _externalEventName, flightPlanApproved);
		string message = flightPlanApproved ? "Flight plan has been approved." : "Flight plan has been disapproved.";
		return request.CreateResponse(HttpStatusCode.OK, message);
	}

	[Function("Orchestrator")]
	public async Task<bool> RunOrchestrationAsync([OrchestrationTrigger] TaskOrchestrationContext context)
	{
		try
		{

			ILogger logger = context.CreateReplaySafeLogger("Orchestrator");
			string notificationEmailAddress = Environment.GetEnvironmentVariable("NotificationEmailAddress")!;
			SubmitFlightPlanDetails submitFlightPlanDetails = context.GetInput<SubmitFlightPlanDetails>()!; ;
			submitFlightPlanDetails.OrchestratorInstanceId = context.InstanceId;

			if (!int.TryParse(Environment.GetEnvironmentVariable("ApprovalTimeoutMinutes"), out int approvalTimeoutMinutes))
				throw new Exception("Approval timeout minutes not configured.");

			SubmittedFlightScheduleTableEntity approvalRequest = await context.CallActivityAsync<SubmittedFlightScheduleTableEntity>("SendApprovalRequest", submitFlightPlanDetails);
			logger.LogInformation("Approval request sent to {emailAddress}.", notificationEmailAddress);

			using CancellationTokenSource timeoutCts = new();
			DateTime expiration = context.CurrentUtcDateTime.AddMinutes(approvalTimeoutMinutes);
			Task timeoutTask = context.CreateTimer(expiration, timeoutCts.Token);

			Task<bool> approvalTask = context.WaitForExternalEvent<bool>(_externalEventName);

			Task winner = await Task.WhenAny(approvalTask, timeoutTask);
			if (winner == approvalTask)
			{
				timeoutCts.Cancel();
				bool isFlightPlanApproved = approvalTask.Result;
				logger.LogInformation("Approval response received: {approved}.", isFlightPlanApproved);
				approvalRequest.FlightPlanAccepted = isFlightPlanApproved;
				await context.CallActivityAsync("UpdateFlightPlanApproval", approvalRequest);
				if (isFlightPlanApproved)
					await context.CallActivityAsync("RecordFlightPlan", approvalRequest);
				return isFlightPlanApproved;
			}
			else
			{
				logger.LogInformation("Approval request timed out.");
				approvalRequest.FlightPlanAccepted = false;
				await context.CallActivityAsync("UpdateFlightPlanApproval", approvalRequest);
				return false;
			}
		}
		catch (Exception ex)
		{
			_logger.LogError("{ex}", ex.Message);
			return false;
		}
	}

	[Function("SendApprovalRequest")]
	[TableOutput("%SubmittedFlightPlansTableName%", Connection = "AzureWebJobsStorage")]
	public static async Task<SubmittedFlightScheduleTableEntity> SendApprovalRequestActivityAsync([ActivityTrigger] SubmitFlightPlanDetails submitFlightPlanDetails)
		=> await SendApprovalRequestAsync(submitFlightPlanDetails);

	[Function("UpdateFlightPlanApproval")]
	public static async Task UpdateFlightPlanApprovalActivityAsync([ActivityTrigger] SubmittedFlightScheduleTableEntity submittedFlightScheduleTableEntity, ILogger logger)
		=> await RecordApprovalAsync(submittedFlightScheduleTableEntity);

	[Function("RecordFlightPlan")]
	public static async Task RecordFlightPlanActivityAsync([ActivityTrigger] SubmittedFlightScheduleTableEntity approvedFlightPlan, ILogger logger)
		=> await RecordFlightPlanAsync(approvedFlightPlan);

	private static async Task<SubmittedFlightScheduleTableEntity> SendApprovalRequestAsync(SubmitFlightPlanDetails submitFlightPlanDetails)
	{

		string emailAddress = Environment.GetEnvironmentVariable("NotificationEmailAddress")!;
		FlightPlanMessage flightPlan = submitFlightPlanDetails.FlightPlan;

		// Build the flight plan approval request email
		StringBuilder emailHtmlBody = new($"Please approve the flight plan for {flightPlan.Airline}-{flightPlan.FlightNumber} from {flightPlan.OriginAirport} to {flightPlan.DestinationAirport} on {flightPlan.DepartureTime:yyyy-MM-dd}.");
		emailHtmlBody.AppendLine("<br /><br />");
		emailHtmlBody.AppendLine($"<ol>");
		emailHtmlBody.AppendLine($"  <li><a href=\"{submitFlightPlanDetails.FunctionHostAddress}/flight-plan/approval/{submitFlightPlanDetails.OrchestratorInstanceId}/true\">Approve</a></li>");
		emailHtmlBody.AppendLine($"  <li><a href=\"{submitFlightPlanDetails.FunctionHostAddress}/flight-plan/approval/{submitFlightPlanDetails.OrchestratorInstanceId}/false\">Disapprove</a></li>");
		emailHtmlBody.AppendLine($"</ol>");

		StringBuilder emailTextBody = new($"Please approve the flight plan for {flightPlan.Airline}-{flightPlan.FlightNumber} from {flightPlan.OriginAirport} to {flightPlan.DestinationAirport} on {flightPlan.DepartureTime:yyyy-MM-dd}.");
		emailTextBody.AppendLine("\n\n");
		emailTextBody.AppendLine($"1. Approve: {submitFlightPlanDetails.FunctionHostAddress}/flight-plan/approval/{submitFlightPlanDetails.OrchestratorInstanceId}/true");
		emailTextBody.AppendLine($"2. Disapprove: {submitFlightPlanDetails.FunctionHostAddress}/flight-plan/approval/{submitFlightPlanDetails.OrchestratorInstanceId}/false");

		// Send the approval request email
		SendEmailRequest sendEmailRequest = new(emailAddress, "Flight Plan Approval Request", emailHtmlBody.ToString(), emailTextBody.ToString());
		string emailOperationId = await SendEmailAsync(sendEmailRequest);

		// Return the submitted flight schedule table entity
		return new()
		{
			PartitionKey = flightPlan.Airline,
			RowKey = emailOperationId,
			FlightPlanAccepted = false,
			Airline = flightPlan.Airline,
			FlightNumber = flightPlan.FlightNumber,
			OriginAirport = flightPlan.OriginAirport,
			DestinationAirport = flightPlan.DestinationAirport,
			AircraftType = flightPlan.AircraftType,
			DepartureTime = flightPlan.DepartureTime,
			ArrivalTime = flightPlan.ArrivalTime,
			Bearing = flightPlan.Bearing,
			RotationSpeed = flightPlan.RotationalSpeed,
			InitialClimbAltitude = flightPlan.InitialClimbAltitude,
			CruiseAltitude = flightPlan.CruiseAltitude,
			StartDescentDistance = flightPlan.StartDescentDistance,
			StartApproachAltitude = flightPlan.StartApproachAltitude,
			LandingSpeed = flightPlan.LandingSpeed,
			CurrentFlightPhase = flightPlan.CurrentFlightPhase
		};

	}

	private static async Task<string> SendEmailAsync(SendEmailRequest sendEmailRequest)
	{
		try
		{
			string emailSendOperationId;
			string connectionString = Environment.GetEnvironmentVariable("ACSConnectionString")!;
			EmailClient emailClient = new(connectionString);
			EmailSendOperation emailSendOperation = await emailClient.SendAsync(
				WaitUntil.Completed,
				Environment.GetEnvironmentVariable("NoReplyEmailAddress")!,
				sendEmailRequest.RecipientAddress,
				sendEmailRequest.Subject,
				sendEmailRequest.HtmlContent,
				sendEmailRequest.PlainTextContent);
			emailSendOperationId = emailSendOperation.Id;
			return emailSendOperationId;
		}
		catch (Exception ex)
		{
			throw new EmailFailedException($"Failed to send the requested email: {ex.Message}", ex);
		}
	}

	private static async Task RecordApprovalAsync(SubmittedFlightScheduleTableEntity submittedFlightPlan)
	{
		TableServiceClient tableServiceClient = new(Environment.GetEnvironmentVariable("AzureWebJobsStorage")!);
		TableClient tableClient = tableServiceClient.GetTableClient("SubmittedFlightPlans");
		await tableClient.UpsertEntityAsync(submittedFlightPlan);
	}

	private static async Task RecordFlightPlanAsync(SubmittedFlightScheduleTableEntity approvedFlightPlan)
	{
		FlightTrackerContext flightTrackerContext = new();
		FlightPlan flightPlan = approvedFlightPlan.ToFlightPlan();
		flightTrackerContext.FlightPlans.Add(flightPlan);
		await flightTrackerContext.SaveChangesAsync();
	}

}