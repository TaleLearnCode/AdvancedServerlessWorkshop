namespace SLS.Marketing.Responses;

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
	public IPostalAddressResponse? PostalAddress { get; set; }

	/// <summary>
	/// The starting price of available apartments at the community.
	/// </summary>
	public decimal StartingAtPrice { get; set; }

	/// <summary>
	/// Pricing for the community by care type, then room type, and then payor type.
	/// </summary>
	public IDictionary<string, IPricingByCareTypeResponse>? Pricing { get; set; } = null!;

}