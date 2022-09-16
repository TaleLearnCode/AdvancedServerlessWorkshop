namespace SLS.Marketing.Responses;

public class CommunityPricingResponse
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
	/// The starting price of available apartments at the community.
	/// </summary>
	public int StartingAtPrice { get; set; }

	/// <summary>
	/// Pricing for the community by care type, then room type, and then payor type.
	/// </summary>
	public Dictionary<string, PricingByCareTypeResponse>? Pricing { get; set; } = null!;

}