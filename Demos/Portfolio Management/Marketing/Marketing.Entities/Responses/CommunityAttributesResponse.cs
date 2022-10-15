namespace SLS.Marketing.Responses;

/// <summary>
/// Represents the response for the GetCommunityDetails endpoint.
/// </summary>
public class CommunityAttributesResponse
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
	/// Listing of the attributes assigned to the community grouped by attribute type.
	/// </summary>
	public Dictionary<string, List<CommunityAttributeResponse>>? Attributes { get; set; }

}