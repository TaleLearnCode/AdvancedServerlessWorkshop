using System.Diagnostics;
using TaleLearnCode.FlightTrackingDemo.Messages;
using TaleLearnCode.FlightTrackingDemo.Simulator.Console.Models;
using TaleLearnCode.FlightTrackingDemo.SqlData.Models;

namespace TaleLearnCode.FlightTrackingDemo.Simulator.Console.Extensions;

internal static class FlightTelemetryExtensions
{
	internal static Geocoordinate Coordinates(this FlightTelemetry telemetry)
		=> new((double)telemetry.Latitude, (double)telemetry.Longitude);

	internal static string LogEntry(this FlightTelemetry flightTelemetry, FlightPlan flightPlan, Dictionary<string, string> flightPhases, Stopwatch flightTimer)
		=> $"{flightPlan.FormattedFlightNumber()} - {flightTimer.Elapsed:hh\\:mm\\:ss} - {flightPhases[flightTelemetry.FlightStatusCode]} - {flightTelemetry.PhaseSequence:N0} - {flightTelemetry.Altitude:N0}m - {flightTelemetry.GroundSpeed:N0}km/h - {flightTelemetry.DistanceSinceLast:N2}km - {flightTelemetry.DistanceSinceOrigin:N1}km - {flightTelemetry.DistanceToDestination:N1}km - {flightTelemetry.Coordinates()}";

	internal static TelemetryMessage ToTelemetryMessage(this FlightTelemetry flightTelemetry, FlightPlan flightPlan)

		=> new()
		{
			FlightPlanId = flightTelemetry.FlightPlanId.ToString(),
			FlightNumber = flightPlan.FlightNumber,
			TelemetryTimestamp = flightTelemetry.TelemetryTimestamp,
			FlightStatusCode = flightTelemetry.FlightStatusCode,
			Longitude = flightTelemetry.Longitude,
			Latitude = flightTelemetry.Latitude,
			Altitude = flightTelemetry.Altitude,
			GroundSpeed = flightTelemetry.GroundSpeed,
			FlightDuration = flightTelemetry.FlightDuration,
			DistanceSinceLast = flightTelemetry.DistanceSinceLast,
			DistanceSinceOrigin = flightTelemetry.DistanceSinceOrigin,
			DistanceToDestination = flightTelemetry.DistanceToDestination,
			PhaseSequence = flightTelemetry.PhaseSequence
		};

}