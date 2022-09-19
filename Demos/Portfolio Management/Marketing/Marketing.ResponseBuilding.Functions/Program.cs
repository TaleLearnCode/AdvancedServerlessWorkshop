using PortfolioContext portfolioContext = new();

string? cosmosConnectionString = Environment.GetEnvironmentVariable("AWS_CosmosConnectionString");
using CosmosClient cosmosClient = new(cosmosConnectionString);
Database cosmosDatabse = await cosmosClient.CreateDatabaseIfNotExistsAsync("ASW");
Container cosmosContainer = await cosmosDatabse.CreateContainerIfNotExistsAsync("Northstar", "/Discriminator");

var host = new HostBuilder()
	.ConfigureFunctionsWorkerDefaults()
	.ConfigureServices(s =>
	{
		s.AddSingleton<ICacheCommunityDetailsResponse>((s) => { return new CacheCommunityDetailsResponse(portfolioContext, cosmosContainer); });
		s.AddSingleton<ICacheCommunityDigitalAssetsResponse>((s) => { return new CacheCommunityDigitalAssetsResponse(portfolioContext, cosmosContainer); });
		s.AddSingleton<ICacheCommunityPricingResponse>((s) => { return new CacheCommunityPricingResponse(portfolioContext, cosmosContainer); });
		s.AddSingleton<ICacheCommunityAttributesResponse>((s) => { return new CacheCommunityAttributesResponse(portfolioContext, cosmosContainer); });
		s.AddSingleton<ICacheCommunityListingResponse>((s) => { return new CacheCommunityListingResponse(portfolioContext, cosmosContainer); });
	})
	.Build();

host.Run();