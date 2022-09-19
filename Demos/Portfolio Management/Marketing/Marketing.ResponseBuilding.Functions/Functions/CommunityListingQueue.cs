namespace SLS.Marketing.ResponseBuilding;

public class CommunityListingQueue
{
	private readonly ILogger _logger;
	private readonly ICacheCommunityListingResponse _services;

	public CommunityListingQueue(
		ILoggerFactory loggerFactory,
		ICacheCommunityListingResponse services)
	{
		_logger = loggerFactory.CreateLogger<CommunityListingQueue>();
		_services = services;
	}

	[Function("CommunityListingQueue")]
	public async Task RunAsync([QueueTrigger("queueCommunityListing", Connection = "StorageConnectionString")] string queueMessage)
	{
		await _services.BuildAsync(queueMessage);
	}

}