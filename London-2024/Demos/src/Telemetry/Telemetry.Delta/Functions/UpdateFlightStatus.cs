using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace TaleLearnCode.FlightTrackingDemo.Telemetry.Delta;

public class UpdateFlightStatus(ILoggerFactory loggerFactory, CosmosClient cosmosClient)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<UpdateFlightStatus>();
	private readonly CosmosClient _cosmosClient = cosmosClient;

	private const string _functionName = "UpdateFlightStatus";

	[Function(_functionName)]
	public async Task RunAsync([CosmosDBTrigger(
		databaseName: "%CosmosTelemetryDatabaseName%",
		containerName: "%CosmosTelemetryContainerName%",
		Connection = "CosmosConnectionString",
		LeaseContainerName = "%FlightStatusLeaseContainerName%",
		CreateLeaseContainerIfNotExists = true)] IReadOnlyList<CosmosData.FlightTelemetry> telemetryRecords)
	{
		if (telemetryRecords is not null && telemetryRecords.Count > 0)
		{
			_logger.LogInformation("[{functionName}] Documents modified: {recordCount}", _functionName, telemetryRecords.Count);
			Database cosmosDatabase = _cosmosClient.GetDatabase(Environment.GetEnvironmentVariable("CosmosFlightStatusDatabaseName"));
			Container cosmosContainer = cosmosDatabase.GetContainer(Environment.GetEnvironmentVariable("CosmosFlightStatusContainerName"));
			FlightTrackerContext context = new();
			Dictionary<string, FlightStatus> flightStatuses = await context.FlightStatuses
				.Include(x => x.CustomerFlightStatusToFlightStatuses)
					.ThenInclude(x => x.CustomerFlightStatusCodeNavigation)
				.ToDictionaryAsync(x => x.FlightStatusCode, x => x);
			foreach (CosmosData.FlightTelemetry telemetryRecord in telemetryRecords)
			{
				_logger.LogInformation("[{functionName}] Updating flight status for flight {airlineCode}-{flightNumber}.", _functionName, telemetryRecord.AirlineCode, telemetryRecord.FlightPlan.FlightNumber);
				CosmosData.CustomerFlightStatus flightStatus = telemetryRecord.ToCustomerFlightStatus(flightStatuses[telemetryRecord.FlightStatusCode]);
				await cosmosContainer.UpsertItemAsync(flightStatus, new PartitionKey(flightStatus.AirlineCode));
			}
		}
	}
}