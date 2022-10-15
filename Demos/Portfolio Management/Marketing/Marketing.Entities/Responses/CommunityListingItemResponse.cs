namespace SLS.Marketing.Responses;

/// <summary>
/// Represents the response for the GetCommunityListing endpoint.
/// </summary>
public class CommunityListingItemResponse
{

	/// <summary>
	/// The publicly identifier number for the community.
	/// </summary>
	public string Number { get; set; } = null!;

	/// <summary>
	/// The name of the community.
	/// </summary>
	public string Name { get; set; } = null!;

	/// <summary>
	/// The publicly listed street address for the community.
	/// </summary>
	public string? StreetAddress { get; set; }

	/// <summary>
	/// The city where community is located.
	/// </summary>
	public string? City { get; set; }

	/// <summary>
	/// The country division where the community is located.
	/// </summary>
	public string? CountryDivision { get; set; }

	/// <summary>
	/// The country where the community is located.
	/// </summary>
	public string? Country { get; set; }

	/// <summary>
	/// The postal code for the country is located.
	/// </summary>
	public string? PostalCode { get; set; }

	/// <summary>
	/// The longitude where the community is located.
	/// </summary>
	public string? Longitude { get; set; }

	/// <summary>
	/// The latitude where the community is located.
	/// </summary>
	public string? Latitude { get; set; }

	/// <summary>
	/// The listing phone number for the community.
	/// </summary>
	public string? PhoneNumber { get; set; }

	/// <summary>
	/// The URL for the profile image of the community.
	/// </summary>
	public Uri? CommunityPhotoUrl { get; set; }

	/// <summary>
	/// The starting price at the community.
	/// </summary>
	public int? StartingAtPrice { get; set; }

	/// <summary>
	/// Flag indicating whether the community is featured for the operator.
	/// </summary>
	public bool IsFeatured { get; set; }

	/// <summary>
	/// List of the types of care provided at the community.
	/// </summary>
	public List<CareTypeItemResponse>? CareTypes { get; set; }

	/// <summary>
	/// Listing of the attributes assigned to the community grouped by attribute type.
	/// </summary>
	public Dictionary<string, List<CommunityAttributeResponse>>? Attributes { get; set; }

}