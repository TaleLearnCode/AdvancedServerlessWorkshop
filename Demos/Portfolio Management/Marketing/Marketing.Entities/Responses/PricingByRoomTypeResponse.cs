namespace SLS.Marketing.Responses;

/// <summary>
/// Represents pricing information for apartments of a particular room type.
/// </summary>
public class PricingByRoomTypeResponse : IPricingByRoomTypeResponse
{

	/// <summary>
	/// The type of apartment whose pricing is being provided.
	/// </summary>
	public string RoomType { get; set; } = null!;

	/// <summary>
	/// The range of the area for apartments of the room type.
	/// </summary>
	/// <remarks>If there are vacant rooms, this value will be the range of available apartments; otherwise, the range will be of all apartments.</remarks>
	public string AreaRange { get; set; } = null!;

	/// <summary>
	/// The starting value of the range of unit area for apartments of the room type.
	/// </summary>
	public int AreaRangeStart { get; set; }

	/// <summary>
	/// The ending value of the range of unit area for apartments of the room type.
	/// </summary>
	public int AreaRangeEnd { get; set; }

	/// <summary>
	/// Number of currently vacant apartments.
	/// </summary>
	public int VacantCount { get; set; }

	/// <summary>
	/// Url of the floor plan for the room type.
	/// </summary>
	public Uri? FloorPlanUrl { get; set; }

	/// <summary>
	/// If true, then pricing can be publicly shown; otherwise, the pricing should not be publicly shown.
	/// </summary>
	public bool ShowPricing { get; set; }

	/// <summary>
	/// The starting value of the pricing range.
	/// </summary>
	public int StartingAt { get; set; }

	/// <summary>
	/// The ending value of the pricing range.
	/// </summary>
	public int EndingAt { get; set; }

	/// <summary>
	/// Detailed pricing by payor type for the room type.
	/// </summary>
	public IDictionary<string, IPricingByPayorTypeResponse> PricingByPayorType { get; set; } = null!;

}