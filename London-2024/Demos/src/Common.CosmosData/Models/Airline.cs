#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;

public class Airline
{

	/// <summary>
	/// The IATA code for the airline.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// The name of the airline.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The callsign for the airline.
	/// </summary>
	public string Callsign { get; set; }

	/// <summary>
	/// The code of the country where the airline is headquartered.
	/// </summary>
	public string CountryCode { get; set; }

	/// <summary>
	/// The name of the country where the airline is headquartered.
	/// </summary>
	public string CountryName { get; set; }

}