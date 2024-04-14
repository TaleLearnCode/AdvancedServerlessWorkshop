using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaleLearnCode.FlightTrackingDemo.Common.CosmosData;

JsonSerializerOptions jsonSerializerOptions = new()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
	WriteIndented = true
};

CosmosClient cosmosClient = new(
	Environment.GetEnvironmentVariable("CosmosConnectionString"),
	new CosmosClientOptions
	{
		Serializer = new CosmosSystemTextJsonSerializer(new JsonSerializerOptions
		{
			Converters = { new JsonStringEnumConverter() },
			PropertyNamingPolicy = jsonSerializerOptions.PropertyNamingPolicy,
			DefaultIgnoreCondition = jsonSerializerOptions.DefaultIgnoreCondition,
			DictionaryKeyPolicy = jsonSerializerOptions.DictionaryKeyPolicy,
			WriteIndented = jsonSerializerOptions.WriteIndented
		})
	});

Database cosmosDatabase = cosmosClient.GetDatabase(Environment.GetEnvironmentVariable("CosmosDatabase"));

IHost host = new HostBuilder()
	.ConfigureFunctionsWebApplication()
	.ConfigureServices(services =>
	{
		services.AddApplicationInsightsTelemetryWorkerService();
		services.ConfigureFunctionsApplicationInsights();
		services.AddSingleton(services => jsonSerializerOptions);
		services.AddSingleton(services => cosmosDatabase);
	})
	.Build();

host.Run();