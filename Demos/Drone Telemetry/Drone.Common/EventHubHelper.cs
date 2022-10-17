namespace Drone.Common
{
	public class EventHubHelper : IEventHubHelper
	{

		private readonly EventHubProducerClient _eventHubProducerClient;
		private readonly JsonSerializerOptions _jsonSerializerOptions;

		public EventHubHelper(
			EventHubProducerClient eventHubProducerClient,
			JsonSerializerOptions jsonSerializer)
		{
			_eventHubProducerClient = eventHubProducerClient;
			_jsonSerializerOptions = jsonSerializer;
		}

		public async Task SendMessageAsync(string message)
		{
			using EventDataBatch eventDataBatch = await _eventHubProducerClient.CreateBatchAsync();
			if (!eventDataBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(message))))
				throw new Exception($"Event is too large for the batch and cannot be sent.");
			await _eventHubProducerClient.SendAsync(eventDataBatch);
		}
	}
}