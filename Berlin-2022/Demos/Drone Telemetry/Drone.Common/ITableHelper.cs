using Azure.Data.Tables;

namespace Drone.Common
{
	public interface ITableHelper<T> where T : ITableEntity
	{
		Task AddEntityAsync(T deviceState);
	}
}