using Azure.Storage.Queues;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using ShellProgressBar;
using SLS.Marketing.ResponseBuilding;
using SLS.Porfolio.Repository;
using SLS.Porfolio.Repository.Entiies;
using System.Text;

Dictionary<string, QueueClient> _queueClients = new();
string _storageConnectionString = "UseDevelopmentStorage=true";

await QueueBuildsAsync();

async Task QueueBuildsAsync()
{
	using PortfolioContext portfolioContext = new();
	List<Community>? communities = await portfolioContext.Communities.ToListAsync();
	Console.WriteLine("Press [Enter] to start...");
	Console.ReadLine();
	using ProgressBar progressBar = new(communities.Count, $"Building responses (1 of {communities.Count})");
	foreach (Community community in communities)
	{
		await SendQueueMessage("marketing-communityattributes", community.CommunityNumber);
		await SendQueueMessage("marketing-communitydetails", community.CommunityNumber);
		await SendQueueMessage("marketing-communitydigitalassets", community.CommunityNumber);
		await SendQueueMessage("marketing-communitylisting", community.CommunityNumber);
		await SendQueueMessage("marketing-communitypricing", community.CommunityNumber);
		progressBar.Tick($"Building responses ({progressBar.CurrentTick + 1} of {progressBar.MaxTicks})");
	}
}

async Task SendQueueMessage(string queueName, string queueMessage)
{
	if (TryGetQueueClient(queueName, out QueueClient? queueClient) && queueClient is not null)
		await queueClient.SendMessageAsync(Base64Encode(queueMessage));
}

bool TryGetQueueClient(string queueName, out QueueClient? queueClient)
{
	if (!_queueClients.TryGetValue(queueName, out queueClient) || queueClient is null)
	{
		_queueClients.TryAdd(queueName, new QueueClient(_storageConnectionString, queueName));
		return _queueClients.TryGetValue(queueName, out queueClient);
	}
	return true;
}

string Base64Encode(string input)
{
	byte[] inputBytes = Encoding.UTF8.GetBytes(input);
	return Convert.ToBase64String(inputBytes);
}

async Task BuildDirectlyAsync()
{

	string? cosmosConnectionString = Environment.GetEnvironmentVariable("ASW_CosmosConnectionString");

	if (cosmosConnectionString is not null)
	{

		using PortfolioContext portfolioContext = new();

		using CosmosClient cosmosClient = new(cosmosConnectionString);
		Database cosmosDatabase = await cosmosClient.CreateDatabaseIfNotExistsAsync("ASW");
		Container container = await cosmosDatabase.CreateContainerIfNotExistsAsync("Northstar", "/Discriminator");

		ICacheCommunityDetailsResponse getCommunityDetails = new CacheCommunityDetailsResponse(container);
		ICacheCommunityDigitalAssetsResponse getCommunityDigitalAssets = new CacheCommunityDigitalAssetsResponse(container);
		ICacheCommunityPricingResponse getCommunityPricing = new CacheCommunityPricingResponse(container);
		ICacheCommunityAttributesResponse getCommunityAttributes = new CacheCommunityAttributesResponse(container);
		ICacheCommunityListingResponse getCommunityListing = new CacheCommunityListingResponse(container);

		List<Community>? communities = await portfolioContext.Communities.ToListAsync();
		using ProgressBar progressBar = new(communities.Count, $"Building responses (1 of {communities.Count})");
		foreach (Community community in communities)
		{
			await getCommunityDetails.BuildAsync(community.CommunityNumber);
			await getCommunityDigitalAssets.BuildAsync(community.CommunityNumber);
			await getCommunityPricing.BuildAsync(community.CommunityNumber);
			await getCommunityAttributes.BuildAsync(community.CommunityNumber);
			await getCommunityListing.BuildAsync(community.CommunityNumber);
			progressBar.Tick($"Building responses ({progressBar.CurrentTick + 1} of {progressBar.MaxTicks})");
		}

	}

}