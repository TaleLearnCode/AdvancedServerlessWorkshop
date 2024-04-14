using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TaleLearnCode.FlightTrackingDemo.Messages;
using TaleLearnCode.FlightTrackingDemo.Telemetry.Receive.OutputTypes;

namespace TaleLearnCode.FlightTrackingDemo.Telemetry.Receive;

public class ReceiveTelemetry(ILogger<ReceiveTelemetry> logger, JsonSerializerOptions jsonSerializerOptions)
{

	private readonly ILogger<ReceiveTelemetry> _logger = logger;
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

	[Function("ReceiveTelemetry")]
	public async Task<ReceiveTelemetryOutput> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request)
	{
		using StreamReader reader = new(request.Body);
		string requestBody = await reader.ReadToEndAsync();
		TelemetryMessage? telemetryMessage = JsonSerializer.Deserialize<TelemetryMessage>(requestBody, _jsonSerializerOptions);
		if (telemetryMessage is not null)
		{
			_logger.LogInformation("Telemetry data received for {0}", telemetryMessage.FlightNumber);
			return new ReceiveTelemetryOutput() { EventHubMessage = JsonSerializer.Serialize(telemetryMessage, _jsonSerializerOptions), ActionResult = new AcceptedResult() };
		}
		else
		{
			_logger.LogWarning("Invalid telemetry data received.");
			return new ReceiveTelemetryOutput() { EventHubMessage = string.Empty, ActionResult = new BadRequestObjectResult("Invalid telemetry data received.") };
		}
	}
}
