#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;

public class Airport
{

	/// <summary>
	/// The IATA code for the airline.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// The name of the airport.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The name of the city where the airport is located.
	/// </summary>
	public string City { get; set; }

	/// <summary>
	/// The code of the country where the airport is located.
	/// </summary>
	public string CountryCode { get; set; }

	/// <summary>
	/// The name of the country where the airport is located.
	/// </summary>
	public string CountryName { get; set; }

	/// <summary>
	/// The elevation of the airport in feet.
	/// </summary>
	public int Elevation { get; set; }

	/// <summary>
	/// The latitude of the airport.
	/// </summary>
	public decimal? Latitude { get; set; }

	/// <summary>
	/// The longitude of the airport.
	/// </summary>
	public decimal? Longitude { get; set; }

}