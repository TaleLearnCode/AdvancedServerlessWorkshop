#nullable disable

namespace TaleLearnCode.FlightTracker.SubmitFlightPlan.Models;

public class SubmittedFlightScheduleTableEntity : ITableEntity
{
	public string PartitionKey { get; set; }
	public string RowKey { get; set; }
	public bool FlightPlanAccepted { get; set; }
	public string Airline { get; set; }
	public string FlightNumber { get; set; }
	public string OriginAirport { get; set; }
	public string DestinationAirport { get; set; }
	public string AircraftType { get; set; }
	public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }
	public double Bearing { get; set; }
	public int RotationSpeed { get; set; }
	public int InitialClimbAltitude { get; set; }
	public int CruiseAltitude { get; set; }
	public int StartDescentDistance { get; set; }
	public int StartApproachAltitude { get; set; }
	public int LandingSpeed { get; set; }
	public string CurrentFlightPhase { get; set; }
	public string OrchestratorInstanceId { get; set; }
	public DateTimeOffset? Timestamp { get; set; }
	public ETag ETag { get; set; }
}