using Drone.Domain.Entities;

namespace Drone.Domain.Repository
{
	public interface IDeviceStateChangeProcessor
	{
		Task UpdateDeviceStateAsync(DeviceState updatedDeviceState);
	}
}