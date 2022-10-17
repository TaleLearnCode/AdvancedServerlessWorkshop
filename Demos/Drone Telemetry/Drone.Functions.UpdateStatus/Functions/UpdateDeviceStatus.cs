namespace Drone.Functions;

public class UpdateDeviceState
{
	private readonly ILogger _logger;
	private readonly JsonSerializerOptions _jsonSerializerOptions;
	private readonly IEventHubHelper _eventHubHelper;

	public UpdateDeviceState(
		ILoggerFactory loggerFactory,
		JsonSerializerOptions jsonSerializerOptions,
		IEventHubHelper eventHubHelper)
	{
		_logger = loggerFactory.CreateLogger<UpdateDeviceState>();
		_jsonSerializerOptions = jsonSerializerOptions;
		_eventHubHelper = eventHubHelper;
	}

	[Function("UpdateDeviceState")]
	public async Task<HttpResponseData> RunAsync(
		[HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData request)
	{
		_logger.LogInformation("UpdateDeviceState: Sending telemetry data to EventHubs");
		try
		{
			DeviceState input = await request.GetRequestParametersAsync<DeviceState>(_jsonSerializerOptions);
			if (input is not null)
			{
				input.LastUpdate ??= DateTime.UtcNow;
				await _eventHubHelper.SendMessageAsync(JsonSerializer.Serialize(input, _jsonSerializerOptions));
				return request.CreateResponse(HttpStatusCode.OK);
			}
			else
			{
				throw new ArgumentNullException("Request Body");
			}
		}
		catch (ArgumentNullException exception)
		{
			return request.CreateBadRequestResponse(exception);
		}
		catch (Exception ex)
		{
			return request.CreateErrorResponse(ex);
		}
	}

}