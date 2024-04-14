using Azure.Data.Tables;

namespace Drone.Common;

public class TableHelper<T> : ITableHelper<T> where T : ITableEntity
{

	private readonly TableClient _tableClient;

	public TableHelper(TableClient tableClient)
	{
		_tableClient = tableClient;
	}

	public async Task AddEntityAsync(T deviceState)
	{
		await _tableClient.AddEntityAsync(deviceState);
	}

}