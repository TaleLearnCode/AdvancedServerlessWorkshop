using Prototype3.Models;
using System.Diagnostics;

namespace Prototype3.Extensions;

internal static class FlightTelemetryExtensions
{
	internal static Geocoordinate Coordinates(this FlightTelemetry telemetry)
		=> new((double)telemetry.Latitude, (double)telemetry.Longitude);

	internal static string LogEntry(this FlightTelemetry flightTelemetry, FlightPlan flightPlan, Dictionary<string, string> flightPhases, Stopwatch flightTimer)
		=> $"{flightPlan.FormattedFlightNumber()} - {flightTimer.Elapsed:hh\\:mm\\:ss} - {flightPhases[flightTelemetry.FlightStatusCode]} - {flightTelemetry.PhaseSequence:N0} - {flightTelemetry.Altitude:N0}m - {flightTelemetry.GroundSpeed:N0}km/h - {flightTelemetry.DistanceSinceLast:N2}km - {flightTelemetry.DistanceSinceOrigin:N1}km - {flightTelemetry.DistanceToDestination:N1}km - {flightTelemetry.Coordinates()}";

}