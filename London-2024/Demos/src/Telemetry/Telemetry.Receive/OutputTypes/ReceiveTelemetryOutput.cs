using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace TaleLearnCode.FlightTrackingDemo.Telemetry.Receive.OutputTypes;

public class ReceiveTelemetryOutput
{

	[EventHubOutput("%EventHubName%", Connection = "EventHubConnection")]
	public required string EventHubMessage { get; set; }

	public required IActionResult ActionResult { get; set; }
}