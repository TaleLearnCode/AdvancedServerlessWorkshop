namespace Drone.Common
{
	public interface IQueueHelper<T>
	{
		Task SendMessageAsync(T queueMessage);
	}
}