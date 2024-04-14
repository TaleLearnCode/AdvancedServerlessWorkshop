#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;

public class AircraftType
{

	/// <summary>
	/// The code for the aircraft type.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// The name of the aircraft type.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The manufacturer of the aircraft type.
	/// </summary>
	public string Manufacturer { get; set; }

}