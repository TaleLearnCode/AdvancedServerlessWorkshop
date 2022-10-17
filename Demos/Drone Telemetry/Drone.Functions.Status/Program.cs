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
	})
	.Build();

host.Run();