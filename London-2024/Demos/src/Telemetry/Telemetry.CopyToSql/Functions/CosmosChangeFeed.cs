using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using CosmosData = TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;

namespace TaleLearnCode.FlightTrackingDemo.Telemetry.CopyToSql.Functions;

public class CosmosChangeFeed
{
	private readonly ILogger _logger;

	public CosmosChangeFeed(ILoggerFactory loggerFactory)
	{
		_logger = loggerFactory.CreateLogger<CosmosChangeFeed>();
	}

	[Function("CosmosChangeFeed")]
	public void Run([CosmosDBTrigger(
		databaseName: "%CosmosDatabaseName%",
		containerName: "containerName",
		Connection = "CosmosConnectionString",
		LeaseContainerName = "leases",
		CreateLeaseContainerIfNotExists = true)] IReadOnlyList<CosmosData.FlightTelemetry> input)
	{
		if (input != null && input.Count > 0)
		{
			_logger.LogInformation("Documents modified: " + input.Count);
			_logger.LogInformation("First document Id: " + input[0].id);
		}
	}
}