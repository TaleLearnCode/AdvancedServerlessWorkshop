namespace Drone.Domain.Entities;

public class EventHubDeadLetterMessage
{
	public Exception Exception { get; set; } = null!;
	public string Message { get; set; } = null!;
	public DeviceState? DeviceState { get; set; }
}