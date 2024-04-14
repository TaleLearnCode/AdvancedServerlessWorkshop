namespace SLS.Marketing.Responses;

/// <summary>
/// Represents care type within a list of care types.
/// </summary>
public class CareTypeItemResponse
{

	/// <summary>
	/// THe code (abbreviation) assigned to the care type.
	/// </summary>
	public string? Code { get; set; }

	/// <summary>
	/// The name of the care type.
	/// </summary>
	public string? Name { get; set; }

}