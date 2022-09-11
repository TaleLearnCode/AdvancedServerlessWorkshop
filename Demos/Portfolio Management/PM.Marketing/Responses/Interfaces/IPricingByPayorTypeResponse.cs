namespace SLS.Portfolio.Marketing.Responses;

/// <summary>
/// Represents a pricing response for the Marketing API.
/// </summary>
public interface IPricingByPayorTypeResponse
{

	/// <summary>
	/// Name of the associated payor type for the pricing response.
	/// </summary>
	string PayorType { get; set; }

	/// <summary>
	/// The starting value of the pricing range.
	/// </summary>
	int StartingAt { get; set; }

	/// <summary>
	/// The ending value of the pricing range.
	/// </summary>
	int EndingAt { get; set; }

}