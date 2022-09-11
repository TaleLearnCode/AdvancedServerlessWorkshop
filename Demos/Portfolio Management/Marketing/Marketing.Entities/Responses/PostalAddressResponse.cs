namespace SLS.Marketing.Responses;

/// <summary>
/// Represents a postal address returned by the SLS Marketing API.
/// </summary>
public class PostalAddressResponse : IPostalAddressResponse
{

	/// <summary>
	/// The first line of the street address.
	/// </summary>
	public string? StreetAddress1 { get; set; }

	/// <summary>
	/// The second line of the street address.
	/// </summary>
	public string? StreetAddress2 { get; set; }

	/// <summary>
	/// Name of the city where the postal address is located.
	/// </summary>
	public string? City { get; set; }

	/// <summary>
	/// Name of the country division where the postal address is located.
	/// </summary>
	public string? CountryDivision { get; set; }

	/// <summary>
	/// Name of the country where the postal address is located.
	/// </summary>
	public string? Country { get; set; }

	/// <summary>
	/// The postal code for the postal address.
	/// </summary>
	public string? PostalCode { get; set; }

}