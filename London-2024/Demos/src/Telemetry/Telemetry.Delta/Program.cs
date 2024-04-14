using Azure.Storage.Blobs;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaleLearnCode.FlightTrackingDemo.Common.CosmosData;

BlobServiceClient blobServiceClient = new(Environment.GetEnvironmentVariable("BlobConnectionString"));
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(Environment.GetEnvironmentVariable("BlobContainerName"));

CosmosClient cosmosClient = new(
	Environment.GetEnvironmentVariable("CosmosConnectionString"),
	new CosmosClientOptions
	{
		Serializer = new CosmosSystemTextJsonSerializer(new JsonSerializerOptions
		{
			Converters = { new JsonStringEnumConverter() },
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true
		})
	});

IHost host = new HostBuilder()
	.ConfigureFunctionsWebApplication()
	.ConfigureServices(services =>
	{
		services.AddApplicationInsightsTelemetryWorkerService();
		services.ConfigureFunctionsApplicationInsights();
		services.AddSingleton(services => containerClient);
		services.AddSingleton(services => cosmosClient);
	})
	.Build();

host.Run();