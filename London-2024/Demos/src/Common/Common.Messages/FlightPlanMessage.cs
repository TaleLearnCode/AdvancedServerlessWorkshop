#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.Messages;

public class FlightPlanMessage
{
	public Guid Id { get; set; }
	public string Airline { get; set; }
	public string FlightNumber { get; set; }
	public string OriginAirport { get; set; }
	public string DestinationAirport { get; set; }
	public string AircraftType { get; set; }
	public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }
	public double Bearing { get; set; }
	public int RotationalSpeed { get; set; }
	public int InitialClimbAltitude { get; set; }
	public int CruiseAltitude { get; set; }
	public int StartDescentDistance { get; set; }
	public int StartApproachAltitude { get; set; }
	public int LandingSpeed { get; set; }
	public string CurrentFlightPhase { get; set; }
}