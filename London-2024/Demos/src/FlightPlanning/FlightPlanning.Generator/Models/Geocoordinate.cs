namespace TaleLearnCode.FlightTrackingDemo.FlightPlanGenerator.Models;

public record Geocoordinate(double Latitude, double Longitude)
{
	public override string ToString() => $"({Latitude:N6}, {Longitude:N6})";
}