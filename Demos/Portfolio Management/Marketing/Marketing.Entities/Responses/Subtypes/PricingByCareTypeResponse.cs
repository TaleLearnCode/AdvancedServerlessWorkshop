﻿namespace SLS.Marketing.Responses;

/// <summary>
/// Pricing information by care type response.
/// </summary>
public class PricingByCareTypeResponse
{

	/// <summary>
	/// The code of the care type.
	/// </summary>
	public string? CareTypeCode { get; set; }

	/// <summary>
	/// The name of the care type.
	/// </summary>
	public string CareTypeName { get; set; } = null!;

	/// <summary>
	/// Pricing for the care type by room type.
	/// </summary>
	public Dictionary<string, PricingByRoomTypeResponse> RoomTypes { get; set; } = null!;

}