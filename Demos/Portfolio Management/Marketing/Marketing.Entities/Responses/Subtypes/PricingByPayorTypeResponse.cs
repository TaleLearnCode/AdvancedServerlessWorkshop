namespace SLS.Marketing.Responses;

/// <summary>
/// Represents a pricing response for the Marketing API.
/// </summary>
public class PricingByPayorTypeResponse
{

	/// <summary>
	/// Name of the associated payor type for the pricing response.
	/// </summary>
	public string PayorType { get; set; } = null!;

	/// <summary>
	/// The starting value of the pricing range.
	/// </summary>
	public int StartingAt { get; set; }

	/// <summary>
	/// The ending value of the pricing range.
	/// </summary>
	public int EndingAt { get; set; }

}