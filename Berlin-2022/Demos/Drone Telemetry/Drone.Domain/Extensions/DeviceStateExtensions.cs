using Drone.Domain.Entities;

namespace Drone.Domain.Extensions;

public static class DeviceStateExtensions
{

	public static bool TryGetTableEntry(this DeviceState deviceState, out ArchivedDeviceState? deviceStateTableEntry)
	{
		deviceStateTableEntry = null;
		if (deviceState.DeviceId is not null && deviceState.LastUpdate is not null)
			deviceStateTableEntry = new()
			{
				PartitionKey = deviceState.DeviceId,
				//RowKey = deviceState.LastUpdate.ToString(),
				//RowKey = "Corina",

				RowKey = ConvertToUnixTimestamp((DateTime)deviceState.LastUpdate).ToString(),
				DeviceId = deviceState.DeviceId,
				LastUpdate = deviceState.LastUpdate,
				Battery = deviceState.Battery,
				FlightMode = deviceState.FlightMode,
				Latitude = deviceState.Latitude,
				Longitude = deviceState.Longitude,
				Altitude = deviceState.Altitude,
				GyrometerOK = deviceState.GyrometerOK,
				AccelerometerOK = deviceState.AccelerometerOK,
				MagnetometerOK = deviceState.MagnetometerOK
			};
		return deviceStateTableEntry is not null;
	}

	public static double ConvertToUnixTimestamp(DateTime dateTime)
	{
		DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		TimeSpan diff = dateTime.ToUniversalTime() - origin;
		return Math.Floor(diff.TotalSeconds);
	}

}