namespace Drone.Functions;

public class TelemetryMonitor
{
	private readonly ILogger _logger;
	private readonly IDeviceStateChangeProcessor _deviceStateChangeProcessor;
	private readonly IQueueHelper<EventHubDeadLetterMessage> _queueHelper;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public TelemetryMonitor(
		ILoggerFactory loggerFactory,
		IDeviceStateChangeProcessor deviceStateChangeProcessor,
		IQueueHelper<EventHubDeadLetterMessage> queueHelper,
		JsonSerializerOptions jsonSerializerOptions)
	{
		_logger = loggerFactory.CreateLogger<TelemetryMonitor>();
		_deviceStateChangeProcessor = deviceStateChangeProcessor;
		_queueHelper = queueHelper;
		_jsonSerializerOptions = jsonSerializerOptions;
	}

	[Function("TelemetryMonitor")]
	public async Task RunAsync(
		[EventHubTrigger("telemetry", Connection = "EventHubConnectionString")] string[] messages)
	{
		foreach (string message in messages)
		{
			DeviceState? input = null;
			try
			{
				input = JsonSerializer.Deserialize<DeviceState>(message, _jsonSerializerOptions);
				if (input is not null)
				{
					await _deviceStateChangeProcessor.UpdateDeviceStateAsync(input);
					_logger.LogInformation("Updated device state for {0}", input.DeviceId);
				}
				else
					throw new ArgumentNullException("input");
			}
			catch (Exception ex)
			{
				await _queueHelper.SendMessageAsync(new()
				{
					Exception = ex,
					Message = message,
					DeviceState = input
				});
				_logger.LogError("Failed to update device state: {0}", ex.Message);
			}
		}
	}

}