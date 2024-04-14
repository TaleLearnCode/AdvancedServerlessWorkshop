namespace TaleLearnCode.FlightTrackingDemo.Telemetry.Process.Functions;

public class ProcessTelemetry(
	ILogger<ProcessTelemetry> logger,
	JsonSerializerOptions jsonSerializerOptions,
	Database cosmosDatabase)
{

	private readonly ILogger<ProcessTelemetry> _logger = logger;
	private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;
	private readonly Database _cosmosDatabase = cosmosDatabase;

	[Function(nameof(ProcessTelemetry))]
	public async Task RunAsync([EventHubTrigger("%EventHubName%", Connection = "EventHubConnectionString", ConsumerGroup = "flight-tracker")] EventData[] messages)
	{
		SqlModels.FlightTrackerContext context = new();
		foreach (EventData message in messages)
		{
			try
			{
				TelemetryMessage? telemetry = JsonSerializer.Deserialize<TelemetryMessage>(Encoding.UTF8.GetString(message.Body.ToArray()), _jsonSerializerOptions);
				if (telemetry is not null)
				{
					if (Guid.TryParse(telemetry.FlightPlanId, out Guid flightPlanId))
					{
						FlightPlan? flightPlan = await GetFlightPlanAsync(context, flightPlanId);
						if (flightPlan is not null)
						{
							SqlModels.FlightStatus? flightStatus = await GetFlightStatusAsync(context, telemetry.FlightStatusCode);
							CosmosModels.FlightTelemetry cosmosTelemetry = await BuildCosmosFlightTelemetryAsync(telemetry, flightPlan, context);
							_logger.LogInformation("Telemetry received for flight {airline}-{flightNumber} at {timestamp}.", flightPlan.AirlineCode, flightPlan.FlightNumber, telemetry.TelemetryTimestamp);
							await SaveCosmosTelemetryAsync(_cosmosDatabase, cosmosTelemetry);
						}
						else
						{
							_logger.LogWarning("Flight plan {flightPlanId} not found.", telemetry.FlightPlanId);
						}
					}
					else
					{
						_logger.LogWarning("Invalid flight plan ID: {flightPlanId}", telemetry.FlightPlanId);
					}
				}
				else
				{
					_logger.LogWarning("Unable to deserialize message: {message}", Encoding.UTF8.GetString(message.Body.ToArray()));
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error processing message");
			}
		}

	}

	private static async Task<FlightPlan?> GetFlightPlanAsync(FlightTrackerContext context, Guid flightPlanId)
	{
		return await context.FlightPlans
			.Include(x => x.OriginAirportCodeNavigation)
				.ThenInclude(x => x.CountryCodeNavigation)
			.Include(x => x.DestinationAirportCodeNavigation)
				.ThenInclude(x => x.CountryCodeNavigation)
			.Include(x => x.AirlineCodeNavigation)
				.ThenInclude(x => x.CountryCodeNavigation)
			.Include(x => x.AircraftTypeCodeNavigation)
			.Include(x => x.CurrentFlightPhaseNavigation)
			.FirstOrDefaultAsync(x => x.FlightPlanId == flightPlanId);
	}

	private async Task<SqlModels.FlightStatus?> GetFlightStatusAsync(SqlModels.FlightTrackerContext context, string flightStatusCode)
	{
		SqlModels.FlightStatus? flightStatus = await context.FlightStatuses
			.FirstOrDefaultAsync(x => x.FlightStatusCode == flightStatusCode);
		if (flightStatus is null)
			_logger.LogWarning("Failed to retrieve the '{flightStatusCode}' flight status.", flightStatusCode);
		return flightStatus;
	}

	private CosmosModels.FlightPlan BuildCosmosFlightPlan(
		TelemetryMessage telemetry,
		SqlModels.FlightPlan flightPlan) => new()
		{
			Id = flightPlan.FlightPlanId.ToString(),
			AirlineCode = flightPlan.AirlineCode,
			Airline = BuildCosmosAirline(flightPlan.AirlineCodeNavigation),
			FlightNumber = flightPlan.FlightNumber,
			OriginAirportCode = flightPlan.OriginAirportCode,
			OriginAirport = BuildCosmosAirport(flightPlan.OriginAirportCodeNavigation),
			DestinationAirportCode = flightPlan.DestinationAirportCode,
			DestinationAirport = BuildCosmosAirport(flightPlan.DestinationAirportCodeNavigation),
			AircraftTypeCode = flightPlan.AircraftTypeCode,
			AircraftType = BuildCosmosAircraftType(flightPlan.AircraftTypeCodeNavigation),
			DepartureTime = flightPlan.DepartureTime,
			ArrivalTime = flightPlan.ArrivalTime,
			Bearing = flightPlan.Bearing,
			RotationalSpeed = flightPlan.RotationalSpeed,
			InitialClimbAltitude = flightPlan.InitialClimbAltitude,
			CruiseAltitude = flightPlan.CruiseAltitude,
			StartDescentDistance = flightPlan.StartDescentDistance,
			StartApproachAltitude = flightPlan.StartApproachAltitude,
			CurrentFlightPhaseCode = flightPlan.CurrentFlightPhase,
			CurrentFlightPhase = BuildCosmosFlightStatus(flightPlan.CurrentFlightPhaseNavigation)
		};

	private static CosmosModels.FlightStatus? BuildCosmosFlightStatus(SqlModels.FlightStatus? flightStatus)
	{
		if (flightStatus is not null)
			return new()
			{
				Code = flightStatus.FlightStatusCode,
				Name = flightStatus.FlightStatusName,
				Description = flightStatus.FlightStatusDescription
			};
		else
			return null;
	}

	private static CosmosModels.Airport BuildCosmosAirport(SqlModels.Airport airport) => new()
	{
		Code = airport.Iatacode,
		Name = airport.AirportName,
		City = airport.CityName,
		CountryCode = airport.CountryCode,
		CountryName = airport.CountryCodeNavigation?.CountryName,
		Latitude = airport.Latitude,
		Longitude = airport.Longitude
	};

	private static CosmosModels.Airline BuildCosmosAirline(SqlModels.Airline airline) => new()
	{
		Code = airline.Iatacode,
		Name = airline.AirlineName,
		CountryCode = airline.CountryCode,
		CountryName = airline.CountryCodeNavigation?.CountryName
	};

	private static CosmosModels.AircraftType BuildCosmosAircraftType(SqlModels.AircraftType aircraftType) => new()
	{
		Code = aircraftType.AircraftTypeCode,
		Name = aircraftType.AircraftTypeName,
		Manufacturer = aircraftType.Manufacturer
	};

	private async Task<CosmosModels.FlightTelemetry> BuildCosmosFlightTelemetryAsync(
		TelemetryMessage telemetry,
		SqlModels.FlightPlan flightPlan,
		SqlModels.FlightTrackerContext context) => new()
		{
			AirlineCode = flightPlan.AirlineCode,
			FlightPlanId = telemetry.FlightPlanId,
			FlightPlan = BuildCosmosFlightPlan(telemetry, flightPlan),
			Timestamp = telemetry.TelemetryTimestamp,
			FlightStatusCode = telemetry.FlightStatusCode,
			FlightStatus = BuildCosmosFlightStatus(await GetFlightStatusAsync(context, telemetry.FlightStatusCode)),
			Longitude = telemetry.Longitude,
			Latitude = telemetry.Latitude,
			Altitude = telemetry.Altitude,
			GroundSpeed = telemetry.GroundSpeed,
			FlightDuration = telemetry.FlightDuration,
			DistanceSinceLast = telemetry.DistanceSinceLast,
			DistanceSinceOrigin = telemetry.DistanceSinceOrigin,
			DistanceToDestination = telemetry.DistanceToDestination,
			PhaseSequence = telemetry.PhaseSequence
		};

	private static async Task SaveCosmosTelemetryAsync(Database database, CosmosModels.FlightTelemetry telemetry)
	{
		Container container = database.GetContainer(telemetry.AirlineCode);
		await container.CreateItemAsync(telemetry, new PartitionKey(telemetry.FlightPlanId));
	}

}