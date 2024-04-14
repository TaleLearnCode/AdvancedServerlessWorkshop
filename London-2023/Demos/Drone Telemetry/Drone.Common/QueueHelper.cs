namespace Drone.Common;

public class QueueHelper<T> : IQueueHelper<T>
{

	private readonly QueueClient _queueClient;

	public QueueHelper(QueueClient queueClient)
	{
		_queueClient = queueClient;
	}

	public async Task SendMessageAsync(T queueMessage)
	{
		await _queueClient.SendMessageAsync(JsonSerializer.Serialize(queueMessage));
	}

}