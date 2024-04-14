namespace TaleLearnCode.FlightTrackingDemo.Telemetry.Delta.Extensions;

internal static class CosmosFlightTelemetryExtensions
{

	public static SqlData.Models.FlightTelemetry ToSqlFlightTelemetry(this CosmosData.FlightTelemetry cosmosFlightTelemetry)
		=> new()
		{
			FlightTelemetryId = Guid.Parse(cosmosFlightTelemetry.Id),
			FlightPlanId = Guid.Parse(cosmosFlightTelemetry.FlightPlanId),
			TelemetryTimestamp = cosmosFlightTelemetry.Timestamp,
			FlightStatusCode = cosmosFlightTelemetry.FlightStatusCode,
			Longitude = cosmosFlightTelemetry.Longitude,
			Latitude = cosmosFlightTelemetry.Latitude,
			Altitude = cosmosFlightTelemetry.Altitude,
			GroundSpeed = cosmosFlightTelemetry.GroundSpeed,
			FlightDuration = cosmosFlightTelemetry.FlightDuration,
			DistanceSinceLast = cosmosFlightTelemetry.DistanceSinceLast,
			DistanceSinceOrigin = cosmosFlightTelemetry.DistanceSinceOrigin,
			DistanceToDestination = cosmosFlightTelemetry.DistanceToDestination,
			PhaseSequence = cosmosFlightTelemetry.PhaseSequence
		};

	public static bool IsDifferentThanSqlRecord(
		this Common.CosmosData.Models.FlightTelemetry cosmosTelemetry,
		SqlData.Models.FlightTelemetry sqlTelemetry)
		=> cosmosTelemetry.Timestamp != sqlTelemetry.TelemetryTimestamp
			|| cosmosTelemetry.FlightStatusCode != sqlTelemetry.FlightStatusCode
			|| cosmosTelemetry.Longitude != sqlTelemetry.Longitude
			|| cosmosTelemetry.Latitude != sqlTelemetry.Latitude
			|| cosmosTelemetry.Altitude != sqlTelemetry.Altitude
			|| cosmosTelemetry.GroundSpeed != sqlTelemetry.GroundSpeed
			|| cosmosTelemetry.FlightDuration != sqlTelemetry.FlightDuration
			|| cosmosTelemetry.DistanceSinceLast != sqlTelemetry.DistanceSinceLast
			|| cosmosTelemetry.DistanceSinceOrigin != sqlTelemetry.DistanceSinceOrigin
			|| cosmosTelemetry.DistanceToDestination != sqlTelemetry.DistanceToDestination
			|| cosmosTelemetry.PhaseSequence != sqlTelemetry.PhaseSequence;

	public static string FileName(this CosmosData.FlightTelemetry cosmosTelemetry)
		=> $"{cosmosTelemetry.FlightPlanId}_{cosmosTelemetry.Id}.json".Replace("-", string.Empty);

	public static CosmosData.CustomerFlightStatus ToCustomerFlightStatus(
		this CosmosData.FlightTelemetry telemetry,
		SqlData.Models.FlightStatus flightStatus)
		=> new()
		{
			Id = $"{telemetry.FlightPlan.DepartureTime:yyyyMMdd}{telemetry.AirlineCode}{telemetry.FlightPlan.FlightNumber}",
			AirlineCode = telemetry.AirlineCode,
			FlightNumber = telemetry.FlightPlan.FlightNumber,
			FlightDate = telemetry.FlightPlan.DepartureTime.ToString("yyyy-MM-dd"),
			Airline = telemetry.FlightPlan.Airline,
			Timestamp = telemetry.Timestamp,
			FlightStatusCode = telemetry.FlightStatusCode,
			FlightStatus = new()
			{
				Code = flightStatus.CustomerFlightStatusToFlightStatuses?.FirstOrDefault()?.CustomerFlightStatusCodeNavigation.CustomerFlightStatusName,
				Name = flightStatus.CustomerFlightStatusToFlightStatuses?.FirstOrDefault()?.CustomerFlightStatusCodeNavigation.CustomerFlightStatusName,
				Description = flightStatus.CustomerFlightStatusToFlightStatuses?.FirstOrDefault()?.CustomerFlightStatusCodeNavigation.CustomerFlightStatusDescription
			},
			FlightPosition = new()
			{
				Longitude = telemetry.Longitude,
				Latitude = telemetry.Latitude,
				Altitude = telemetry.Altitude,
				GroundSpeed = telemetry.GroundSpeed
			},
			DepartureTime = telemetry.FlightPlan.DepartureTime,
			ArrivalTime = telemetry.FlightPlan.ArrivalTime,
			OriginAirportCode = telemetry.FlightPlan.OriginAirportCode,
			OriginAirport = telemetry.FlightPlan.OriginAirport,
			DestinationAirportCode = telemetry.FlightPlan.DestinationAirportCode,
			DestinationAirport = telemetry.FlightPlan.DestinationAirport,
			AircraftTypeCode = telemetry.FlightPlan.AircraftTypeCode,
			AircraftType = telemetry.FlightPlan.AircraftType,
			DistanceSingeOrigin = telemetry.DistanceSinceOrigin,
			DistanceToDestination = telemetry.DistanceToDestination
		};

}