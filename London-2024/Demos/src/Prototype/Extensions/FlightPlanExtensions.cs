using Prototype3.Models;

namespace Prototype3.Extensions;

internal static class FlightPlanExtensions
{
	public static Geocoordinate OriginCoordinates(this FlightPlan flightPlan)
		=> new((double)flightPlan.OriginAirportCodeNavigation.Latitude, (double)flightPlan.OriginAirportCodeNavigation.Longitude);

	public static Geocoordinate DestinationCoordinates(this FlightPlan flightPlan)
		=> new((double)flightPlan.DestinationAirportCodeNavigation.Latitude, (double)flightPlan.DestinationAirportCodeNavigation.Longitude);

	public static FlightTelemetry InitialFlightTelemetry(this FlightPlan flightPlan) => new()
	{
		FlightPlanId = flightPlan.FlightPlanId,
		TelemetryTimestamp = flightPlan.DepartureTime,
		FlightStatusCode = flightPlan.CurrentFlightPhase,
		Latitude = flightPlan.OriginAirportCodeNavigation.Latitude,
		Longitude = flightPlan.OriginAirportCodeNavigation.Longitude,
		Altitude = flightPlan.OriginAirportCodeNavigation.Elevation,
		GroundSpeed = 0,
		FlightDuration = 0,
		DistanceSinceLast = 0
	};

	public static string FormattedFlightNumber(this FlightPlan flightPlan)
		=> $"{flightPlan.AirlineCode}-{flightPlan.FlightNumber}";

}