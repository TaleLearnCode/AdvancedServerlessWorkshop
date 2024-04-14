namespace TaleLearnCode.FlightTrackingDemo.Messages;

/// <summary>
/// Message to store the Telemetry data for a flight.
/// </summary>
public class TelemetryMessage
{

	/// <summary>
	/// Identifier of the Flight Plan for the Telemetry data.
	/// </summary>
	public string FlightPlanId { get; set; } = null!;

	/// <summary>
	/// The number of the flight.
	/// </summary>
	public string FlightNumber { get; set; } = null!;

	/// <summary>
	/// Timestamp of the Telemetry data.
	/// </summary>
	public DateTime TelemetryTimestamp { get; set; }

	/// <summary>
	/// The status of the flight at the time of the Telemetry data.
	/// </summary>
	public string FlightStatusCode { get; set; } = null!;

	/// <summary>
	/// The longitude of the aircraft at the time of the Telemetry data.
	/// </summary>
	public decimal Longitude { get; set; }

	/// <summary>
	/// The latitude of the aircraft at the time of the Telemetry data.
	/// </summary>
	public decimal Latitude { get; set; }

	/// <summary>
	/// The altitude of the aircraft at the time of the Telemetry data.
	/// </summary>
	public int Altitude { get; set; }

	/// <summary>
	/// The speed of the aircraft relative to the ground at the time of the Telemetry data.
	/// </summary>
	public int GroundSpeed { get; set; }

	/// <summary>
	/// The duration of the flight in milliseconds at the time of the Telemetry data.
	/// </summary>
	public int FlightDuration { get; set; }

	/// <summary>
	/// The distance traveled in kilometers since the last Telemetry data.
	/// </summary>
	public double DistanceSinceLast { get; set; }

	/// <summary>
	/// The distance traveled in kilometers since the origin of the flight.
	/// </summary>
	public double DistanceSinceOrigin { get; set; }

	/// <summary>
	/// The distance remaining in kilometers to the destination of the flight.
	/// </summary>
	public double DistanceToDestination { get; set; }

	/// <summary>
	/// The sequential count of data recordings within the flight phase.
	/// </summary>
	public int PhaseSequence { get; set; }

}