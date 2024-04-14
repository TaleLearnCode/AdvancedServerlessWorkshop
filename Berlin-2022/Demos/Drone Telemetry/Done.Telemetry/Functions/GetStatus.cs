namespace Done.Telemetry;

public class GetStatus
{
	private readonly ILogger _logger;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public GetStatus(
		ILoggerFactory loggerFactory,
		JsonSerializerOptions jsonSerializerOptions)
	{
		_logger = loggerFactory.CreateLogger<GetStatus>();
		_jsonSerializerOptions = jsonSerializerOptions;
	}

	[Function("GetStatus")]
	public async Task<HttpResponseData> Run(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "{deviceId}")] HttpRequestData request,
		string deviceId,
		[CosmosDBInput(
			databaseName: "ASW",
		collectionName: "DroneTelemetry",
		ConnectionStringSetting = "CosmosConnectionString",
		Id ="{deviceId}",
		PartitionKey ="{deviceId}")] DeviceState deviceState)
	{
		_logger.LogInformation("Retrieving current status for device");
		try
		{
			ArgumentNullException.ThrowIfNull(deviceId);
			if (deviceState is not null)
				return await request.CreateResponseAsync(deviceState, _jsonSerializerOptions);
			else
				throw new ArgumentOutOfRangeException();
		}
		catch (ArgumentNullException ex)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (ArgumentOutOfRangeException)
		{
			return request.CreateResponse(HttpStatusCode.NotFound);
		}
	}

}