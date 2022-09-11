namespace SLS.Portfolio.Marketing.Responses;

/// <summary>
/// Represents a postal address returned by the SLS Marketing API.
/// </summary>
public interface IPostalAddressResponse
{

	/// <summary>
	/// The first line of the street address.
	/// </summary>
	string? StreetAddress1 { get; set; }

	/// <summary>
	/// The second line of the street address.
	/// </summary>
	string? StreetAddress2 { get; set; }

	/// <summary>
	/// Name of the city where the postal address is located.
	/// </summary>
	string? City { get; set; }

	/// <summary>
	/// Name of the country division where the postal address is located.
	/// </summary>
	string? CountryDivision { get; set; }

	/// <summary>
	/// Name of the country where the postal address is located.
	/// </summary>
	string? Country { get; set; }

	/// <summary>
	/// The postal code for the postal address.
	/// </summary>
	string? PostalCode { get; set; }

}