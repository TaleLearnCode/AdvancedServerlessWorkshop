#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;

public class FlightStatus
{

	/// <summary>
	/// The code for the flight status.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// The name of the flight status.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The description of the flight status.
	/// </summary>
	public string Description { get; set; }

}