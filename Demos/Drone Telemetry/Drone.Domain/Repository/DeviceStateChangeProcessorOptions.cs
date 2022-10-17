namespace Drone.Domain.Repository;

public class DeviceStateChangeProcessorOptions
{
	public string DatabaseName { get; set; } = null!;
	public string ContainerName { get; set; } = null!;
}