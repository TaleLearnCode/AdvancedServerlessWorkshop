namespace SLS.Marketing.Responses;

/// <summary>
/// Pricing information by care type response.
/// </summary>
public interface IPricingByCareTypeResponse
{

	/// <summary>
	/// The code of the care type.
	/// </summary>
	string? CareTypeCode { get; set; }

	/// <summary>
	/// The name of the care type.
	/// </summary>
	string CareTypeName { get; set; }

	/// <summary>
	/// Pricing for the care type by room type.
	/// </summary>
	IDictionary<string, IPricingByRoomTypeResponse> RoomTypes { get; set; }

}