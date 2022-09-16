JsonSerializerOptions jsonSerializerOptions = new()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
	WriteIndented = true
};

string? cosmosConnectionString = Environment.GetEnvironmentVariable("AWS_CosmosConnectionString");
using CosmosClient cosmosClient = new(cosmosConnectionString);
Database cosmosDatabse = await cosmosClient.CreateDatabaseIfNotExistsAsync("ASW");
Container cosmosContainer = await cosmosDatabse.CreateContainerIfNotExistsAsync("Northstar", "/Discriminator");

var host = new HostBuilder()
	.ConfigureFunctionsWorkerDefaults()
	.ConfigureServices(s =>
	{
		s.AddSingleton((s) => { return jsonSerializerOptions; });
		s.AddSingleton<ICommunityDetailsServices>((s) => { return new CommunityDetailsServices(cosmosContainer); });
		s.AddSingleton<ICommunityDigitalAssetsServices>((s) => { return new CommunityDigitalAssetsServices(cosmosContainer); });
	})
	.Build();

host.Run();