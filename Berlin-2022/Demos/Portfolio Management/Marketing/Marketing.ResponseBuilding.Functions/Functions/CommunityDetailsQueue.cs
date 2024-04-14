namespace SLS.Marketing.ResponseBuilding;

public class CommunityDetailsQueue
{
	private readonly ILogger _logger;
	private readonly ICacheCommunityDetailsResponse _services;

	public CommunityDetailsQueue(
		ILoggerFactory loggerFactory,
		ICacheCommunityDetailsResponse services)
	{
		_logger = loggerFactory.CreateLogger<CommunityDetailsQueue>();
		_services = services;
	}

	[Function("CommunityDetailsQueue")]
	public async Task RunAsync([QueueTrigger("%queueCommunityDetails%", Connection = "StorageConnectionString")] string queueMessage)
	{
		await _services.BuildAsync(queueMessage);
	}

}