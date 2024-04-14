using Azure;
using Azure.Data.Tables;

namespace Drone.Domain.Entities;

public class ArchivedDeviceState : DeviceState, ITableEntity
{
	public string PartitionKey { get; set; } = null!;
	public string RowKey { get; set; } = null!;
	public ETag ETag { get; set; } = default!;
	public DateTimeOffset? Timestamp { get; set; } = default!;
}