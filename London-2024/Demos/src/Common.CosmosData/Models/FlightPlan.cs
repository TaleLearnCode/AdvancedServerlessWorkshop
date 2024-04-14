#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;

public class FlightPlan
{

	/// <summary>
	/// Identifier of the flight plan.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// The IATA code of the airline.
	/// </summary>
	public string AirlineCode { get; set; }

	/// <summary>
	/// The details of the airline.
	/// </summary>
	public Airline Airline { get; set; }

	/// <summary>
	/// The flight number assigned by the airline.
	/// </summary>
	public string FlightNumber { get; set; }

	/// <summary>
	/// The IATA code of the airport where the flight departs.
	/// </summary>
	public string OriginAirportCode { get; set; }

	/// <summary>
	/// Details of the origin airport.
	/// </summary>
	public Airport OriginAirport { get; set; }

	/// <summary>
	/// The IATA code of the airport where the flight arrives.
	/// </summary>
	public string DestinationAirportCode { get; set; }

	/// <summary>
	/// Details of the destination airport.
	/// </summary>
	public Airport DestinationAirport { get; set; }

	/// <summary>
	/// The type of aircraft used for the flight.
	/// </summary>
	public string AircraftTypeCode { get; set; }

	/// <summary>
	/// Details of the aircraft type.
	/// </summary>
	public AircraftType AircraftType { get; set; }

	/// <summary>
	/// The date and time when the flight departs.
	/// </summary>
	public DateTime DepartureTime { get; set; }

	/// <summary>
	/// The date and time when the flight arrives.
	/// </summary>
	public DateTime ArrivalTime { get; set; }

	/// <summary>
	/// The direction of the flight in degrees.
	/// </summary>
	public double Bearing { get; set; }

	/// <summary>
	/// The speed at which the aircraft rotates during takeoff.
	/// </summary>
	public int RotationalSpeed { get; set; }

	/// <summary>
	/// The altitude at which the aircraft starts climbing after takeoff.
	/// </summary>
	public int InitialClimbAltitude { get; set; }

	/// <summary>
	/// The altitude at which the aircraft cruises during the flight.
	/// </summary>
	public int CruiseAltitude { get; set; }

	/// <summary>
	/// The distance from the destination airport at which the aircraft starts descending.
	/// </summary>
	public int StartDescentDistance { get; set; }

	/// <summary>
	/// The altitude at which the aircraft starts the approach to the destination airport.
	/// </summary>
	public int StartApproachAltitude { get; set; }

	/// <summary>
	/// The current phase of the flight.
	/// </summary>
	public string CurrentFlightPhaseCode { get; set; }

	/// <summary>
	/// Gets the details of the current phase of the flight.
	/// </summary>
	public FlightStatus CurrentFlightPhase { get; set; }

	/// <summary>
	/// The speed at which the aircraft lands.
	/// </summary>
	public int LandingSpeed { get; set; }

}