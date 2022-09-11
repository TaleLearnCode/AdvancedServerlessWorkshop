namespace SLS.Marketing.Responses;

/// <summary>
/// Represents pricing information for apartments of a particular room type.
/// </summary>
public interface IPricingByRoomTypeResponse
{

	/// <summary>
	/// The type of apartment whose pricing is being provided.
	/// </summary>
	string RoomType { get; set; }

	/// <summary>
	/// The range of the area for apartments of the room type.
	/// </summary>
	/// <remarks>If there are vacant rooms, this value will be the range of available apartments; otherwise, the range will be of all apartments.</remarks>
	string AreaRange { get; set; }

	/// <summary>
	/// Number of currently vacant apartments.
	/// </summary>
	int VacantCount { get; set; }

	/// <summary>
	/// Url of the floor plan for the room type.
	/// </summary>
	Uri? FloorPlanUrl { get; set; }

	/// <summary>
	/// If true, then pricing can be publicly shown; otherwise, the pricing should not be publicly shown.
	/// </summary>
	bool ShowPricing { get; set; }

	/// <summary>
	/// The starting value of the pricing range.
	/// </summary>
	int StartingAt { get; set; }

	/// <summary>
	/// The ending value of the pricing range.
	/// </summary>
	int EndingAt { get; set; }

	/// <summary>
	/// Detailed pricing by payor type for the room type.
	/// </summary>
	IDictionary<string, IPricingByPayorTypeResponse> PricingByPayorType { get; set; }

}