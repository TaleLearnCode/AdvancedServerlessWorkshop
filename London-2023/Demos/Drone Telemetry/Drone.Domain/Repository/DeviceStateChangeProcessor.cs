using Drone.Domain.Entities;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Drone.Domain.Repository;

public class DeviceStateChangeProcessor : IDeviceStateChangeProcessor
{

	private readonly CosmosClient _cosmosClient;
	private readonly string _databaseName;
	private readonly string _containerName;

	public DeviceStateChangeProcessor(
		CosmosClient cosmosClient,
		DeviceStateChangeProcessorOptions options)
	{
		_cosmosClient = cosmosClient;
		_databaseName = options.DatabaseName;
		_containerName = options.ContainerName;
	}

	public async Task UpdateDeviceStateAsync(DeviceState updatedDeviceState)
	{
		Container container = await GetContainerAsync();
		DeviceState? currentDeviceState = await (GetCurrentStateAsync(container, updatedDeviceState.DeviceId));
		await container.UpsertItemAsync(FillValues(currentDeviceState, updatedDeviceState), new PartitionKey(updatedDeviceState.DeviceId));
	}

	private async Task<Container> GetContainerAsync()
	{
		Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
		Container container = await database.CreateContainerIfNotExistsAsync(
			id: _containerName,
			partitionKeyPath: "/id"
		);
		return container;
	}

	private async Task<DeviceState?> GetCurrentStateAsync(Container container, string deviceId)
	{
		try
		{
			ItemResponse<DeviceState> itemResponse = await container.ReadItemAsync<DeviceState>(id: deviceId, partitionKey: new PartitionKey(deviceId));
			if (itemResponse.StatusCode == HttpStatusCode.OK)
				return itemResponse.Resource;
			else
				return null;
		}
		catch (CosmosException ex)
		{
			if (ex.StatusCode == HttpStatusCode.NotFound)
				return null;
			else
				throw;
		}
	}

	private DeviceState FillValues(DeviceState? current, DeviceState updated)
	{
		if (current is not null)
		{
			updated.Battery = updated.Battery ?? current.Battery;
			updated.FlightMode = updated.FlightMode ?? current.FlightMode;
			updated.Latitude = updated.Latitude ?? current.Latitude;
			updated.Longitude = updated.Longitude ?? current.Longitude;
			updated.Altitude = updated.Altitude ?? current.Altitude;
			updated.AccelerometerOK = updated.AccelerometerOK ?? current.AccelerometerOK;
			updated.GyrometerOK = updated.GyrometerOK ?? current.GyrometerOK;
			updated.MagnetometerOK = updated.MagnetometerOK ?? current.MagnetometerOK;
		}
		return updated;
	}

}