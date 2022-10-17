using Azure.Data.Tables;

EventHubProducerClient eventHubProducerClient = new(Environment.GetEnvironmentVariable("EventHubConnectionString"), "telemetry");

CosmosClient cosmosClient = new(Environment.GetEnvironmentVariable("CosmosConnectionString"));
DeviceStateChangeProcessorOptions deviceStateChangeProcessorOptions = new()
{
	DatabaseName = Environment.GetEnvironmentVariable("CosmosDatabaseName") ?? "AWS",
	ContainerName = Environment.GetEnvironmentVariable("CosmosContainerName") ?? "Telemetry"
};

QueueClient queueClient = new(Environment.GetEnvironmentVariable("StorageConnectionString"), Environment.GetEnvironmentVariable("EventHubDeadLetterQueue"));

// New instance of the TableClient class
TableServiceClient tableServiceClient = new TableServiceClient(Environment.GetEnvironmentVariable("StorageConnectionString"));
TableClient tableClient = tableServiceClient.GetTableClient(
		tableName: "DroneTelemetryArchive"
);

await tableClient.CreateIfNotExistsAsync();

JsonSerializerOptions jsonSerializerOptions = new()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
};

var host = new HostBuilder()
	.ConfigureFunctionsWorkerDefaults()
	.ConfigureServices(s =>
	{
		s.AddSingleton((s) => { return jsonSerializerOptions; });
		s.AddSingleton<IEventHubHelper>((s) => { return new EventHubHelper(eventHubProducerClient, jsonSerializerOptions); });
		s.AddSingleton<IQueueHelper<EventHubDeadLetterMessage>>((s) => { return new QueueHelper<EventHubDeadLetterMessage>(queueClient); });
		s.AddSingleton<IDeviceStateChangeProcessor>((s) => { return new DeviceStateChangeProcessor(cosmosClient, deviceStateChangeProcessorOptions); });
		s.AddSingleton<ITableHelper<ArchivedDeviceState>>((s) => { return new TableHelper<ArchivedDeviceState>(tableClient); });
	})
	.Build();

host.Run();