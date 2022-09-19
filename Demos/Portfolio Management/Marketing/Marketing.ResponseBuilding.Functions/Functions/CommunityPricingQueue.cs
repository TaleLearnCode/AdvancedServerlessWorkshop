namespace SLS.Marketing.ResponseBuilding;

public class CommunityPricingQueue
{
	private readonly ILogger _logger;
	private readonly ICacheCommunityPricingResponse _services;

	public CommunityPricingQueue(
		ILoggerFactory loggerFactory,
		ICacheCommunityPricingResponse services)
	{
		_logger = loggerFactory.CreateLogger<CommunityPricingQueue>();
		_services = services;
	}

	[Function("CommunityPricingQueue")]
	public async Task RunAsync([QueueTrigger("queueCommunityPricing", Connection = "StorageConnectionString")] string queueMessage)
	{
		await _services.BuildAsync(queueMessage);
	}

}