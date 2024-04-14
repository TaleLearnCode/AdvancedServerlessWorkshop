namespace TaleLearnCode.FlightTrackingDemo.Telemetry.Delta.Functions;

public class CopyToSqlServer(ILoggerFactory loggerFactory)
{

	private readonly ILogger _logger = loggerFactory.CreateLogger<CopyToSqlServer>();

	[Function("CopyToSqlServer")]
	public async Task RunAsync([CosmosDBTrigger(
		databaseName: "%CosmosTelemetryDatabaseName%",
		containerName: "%CosmosTelemetryContainerName%",
		Connection = "CosmosConnectionString",
		LeaseContainerName = "%SqlLeaseContainerName%",
		CreateLeaseContainerIfNotExists = true)] IReadOnlyList<CosmosData.FlightTelemetry> telemetryRecords)
	{
		if (telemetryRecords is not null && telemetryRecords.Count > 0)
		{
			_logger.LogInformation("Documents modified: " + telemetryRecords.Count);
			FlightTrackerContext context = new();
			foreach (CosmosData.FlightTelemetry telemetryRecord in telemetryRecords)
			{
				SqlData.Models.FlightTelemetry? flightTelemetry = context.FlightTelemetries
					.Where(f => f.FlightTelemetryId == Guid.Parse(telemetryRecord.Id))
					.FirstOrDefault();
				if (flightTelemetry is null)
				{
					await context.FlightTelemetries.AddAsync(telemetryRecord.ToSqlFlightTelemetry());
					_logger.LogInformation("Adding new record for Flight Plan ID: {FlightPlanId}", telemetryRecord.FlightPlanId);
				}
				else if (telemetryRecord.IsDifferentThanSqlRecord(flightTelemetry))
				{
					UpdateSqlRecord(context, telemetryRecord, flightTelemetry);
					_logger.LogInformation("Updating record for Flight Plan ID: {FlightPlanId}", telemetryRecord.FlightPlanId);
				}
			}
			await context.SaveChangesAsync();
		}
	}

	private static void UpdateSqlRecord(
		FlightTrackerContext context,
		CosmosData.FlightTelemetry telemetryRecord,
		FlightTelemetry flightTelemetry)
	{
		flightTelemetry.TelemetryTimestamp = telemetryRecord.Timestamp;
		flightTelemetry.FlightStatusCode = telemetryRecord.FlightStatusCode;
		flightTelemetry.Longitude = telemetryRecord.Longitude;
		flightTelemetry.Latitude = telemetryRecord.Latitude;
		flightTelemetry.Altitude = telemetryRecord.Altitude;
		flightTelemetry.GroundSpeed = telemetryRecord.GroundSpeed;
		flightTelemetry.FlightDuration = telemetryRecord.FlightDuration;
		flightTelemetry.DistanceSinceLast = telemetryRecord.DistanceSinceLast;
		flightTelemetry.DistanceSinceOrigin = telemetryRecord.DistanceSinceOrigin;
		flightTelemetry.DistanceToDestination = telemetryRecord.DistanceToDestination;
		flightTelemetry.PhaseSequence = telemetryRecord.PhaseSequence;
		context.FlightTelemetries.Update(flightTelemetry);
	}
}