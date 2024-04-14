namespace SLS.Marketing.ResponseBuilding;

public class CommunityDigitalAssetsQueue
{
	private readonly ILogger _logger;
	private readonly ICacheCommunityDigitalAssetsResponse _services;

	public CommunityDigitalAssetsQueue(
		ILoggerFactory loggerFactory,
		ICacheCommunityDigitalAssetsResponse services)
	{
		_logger = loggerFactory.CreateLogger<CommunityDigitalAssetsQueue>();
		_services = services;

	}

	[Function("CommunityDigitalAssetsQueue")]
	public async Task RunAsync([QueueTrigger("%queueCommunityDigitalAssets%", Connection = "StorageConnectionString")] string queueMessage)
	{
		await _services.BuildAsync(queueMessage);
	}

}