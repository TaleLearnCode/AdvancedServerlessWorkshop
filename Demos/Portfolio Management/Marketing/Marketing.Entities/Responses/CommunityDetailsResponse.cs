namespace SLS.Marketing.Responses;

/// <summary>
/// Represents the response for the GetCommunityDetails endpoint.
/// </summary>
public class CommunityDetailsResponse
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
	/// The primary, publicly-listed phone for the community.
	/// </summary>
	public string? PhoneNumber { get; set; }

	/// <summary>
	/// The primary, publicly-listed postal address for the community.
	/// </summary>
	public PostalAddressResponse? PostalAddress { get; set; }

	/// <summary>
	/// The starting price of available apartments at the community.
	/// </summary>
	public decimal StartingAtPrice { get; set; }

	/// <summary>
	/// Pricing for the community by care type, then room type, and then payor type.
	/// </summary>
	public Dictionary<string, PricingByCareTypeResponse>? Pricing { get; set; } = null!;

	/// <summary>
	/// Listing of the community digital assets grouped by the digital asset type.
	/// </summary>
	public Dictionary<string, List<DigitalAssetResponse>>? DigitalAssets { get; set; }

	/// <summary>
	/// Listing of the attributes assigned to the community grouped by attribute type.
	/// </summary>
	public Dictionary<string, List<CommunityAttributeResponse>>? Attributes { get; set; }

}