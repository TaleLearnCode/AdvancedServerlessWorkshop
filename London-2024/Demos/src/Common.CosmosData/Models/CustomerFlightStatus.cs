#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;

public class CustomerFlightStatus
{
	public string Id { get; set; }
	public string AirlineCode { get; set; }
	public string FlightNumber { get; set; }
	public string FlightDate { get; set; }
	public Airline Airline { get; set; }
	public DateTime Timestamp { get; set; }
	public string FlightStatusCode { get; set; }
	public FlightStatus FlightStatus { get; set; }
	public FlightPosition FlightPosition { get; set; }
	public DateTime DepartureTime { get; set; }
	public string OriginAirportCode { get; set; }
	public Airport OriginAirport { get; set; }
	public DateTime ArrivalTime { get; set; }
	public string DestinationAirportCode { get; set; }
	public Airport DestinationAirport { get; set; }
	public string AircraftTypeCode { get; set; }
	public AircraftType AircraftType { get; set; }
	public double DistanceSingeOrigin { get; set; }
	public double DistanceToDestination { get; set; }
}