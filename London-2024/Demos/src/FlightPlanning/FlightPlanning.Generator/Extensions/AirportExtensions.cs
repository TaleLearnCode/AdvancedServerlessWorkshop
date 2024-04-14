using TaleLearnCode.FlightTrackingDemo.FlightPlanGenerator.Models;
using TaleLearnCode.FlightTrackingDemo.SqlData.Models;

namespace TaleLearnCode.FlightTrackingDemo.FlightPlanGenerator.Extensions;

internal static class AirportExtensions
{
	internal static Geocoordinate Coordinates(this Airport airport) => new((double)airport.Latitude, (double)airport.Longitude);
}