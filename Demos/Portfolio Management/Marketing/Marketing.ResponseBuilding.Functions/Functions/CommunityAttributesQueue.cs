namespace SLS.Marketing.ResponseBuilding;

public class CommunityAttributesQueue
{
	private readonly ILogger _logger;
	private readonly ICacheCommunityAttributesResponse _services;

	public CommunityAttributesQueue(
		ILoggerFactory loggerFactory,
		ICacheCommunityAttributesResponse services)
	{
		_logger = loggerFactory.CreateLogger<CommunityAttributesQueue>();
		_services = services;
	}

	[Function("CommunityAttributesQueue")]
	public async Task RunAsync([QueueTrigger("queueCommunityAttributes", Connection = "StorageConnectionString")] string queueMessage)
	{
		await _services.BuildAsync(queueMessage);
	}

}