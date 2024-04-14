using TaleLearnCode.FlightTrackingDemo.Simulator.Console.Models;
using TaleLearnCode.FlightTrackingDemo.SqlData.Models;

namespace TaleLearnCode.FlightTrackingDemo.Simulator.Console.Extensions;

internal static class AirportExtensions
{
	internal static Geocoordinate Coordinates(this Airport airport) => new((double)airport.Latitude, (double)airport.Longitude);
}