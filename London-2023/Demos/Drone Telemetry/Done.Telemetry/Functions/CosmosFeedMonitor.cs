namespace Done.Telemetry;

public class CosmosFeedMonitor
{
	private readonly ILogger _logger;
	private readonly ITableHelper<ArchivedDeviceState> _tableHelper;

	public CosmosFeedMonitor(
		ILoggerFactory loggerFactory,
		ITableHelper<ArchivedDeviceState> tableHelper)
	{
		_logger = loggerFactory.CreateLogger<CosmosFeedMonitor>();
		_tableHelper = tableHelper;
	}

	[Function("CosmosFeedMonitor")]
	public async Task RunAsync([CosmosDBTrigger(
		databaseName: "ASW",
		collectionName: "DroneTelemetry",
		ConnectionStringSetting = "CosmosConnectionString",
		LeaseCollectionName = "leases",
		CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<DeviceState> updates)
	{
		if (updates is not null && updates.Count > 0)
		{
			foreach (DeviceState update in updates)
			{
				try
				{
					if (update.TryGetTableEntry(out ArchivedDeviceState? tableEntry) && tableEntry is not null)
					{
						_logger.LogInformation("Archiving device state for {0}", update.DeviceId);
						await _tableHelper.AddEntityAsync(tableEntry);
					}
					else
					{
						_logger.LogWarning("Unable to archive device state for {0}", update.DeviceId);
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
				}
			}
		}
	}

	[TableOutput("DroneTelemetryArchive", Connection = "StorageConnectionString")]
	public ArchivedDeviceState RecordTableEntry(ArchivedDeviceState deviceState)
	{
		_logger.LogInformation("Archiving device state for {0}", deviceState.DeviceId);
		return deviceState;
	}

}