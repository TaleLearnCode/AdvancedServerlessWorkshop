namespace SLS.Marketing.Responses;

/// <summary>
/// Represents the response for the GetCommunityDigitalAssets endpoint.
/// </summary>
public class CommunityDigitalAssetsResponse
{

	/// <summary>
	/// The number of the referenced community.
	/// </summary>
	public string Number { get; set; } = null!;

	/// <summary>
	/// The name of the referenced community.
	/// </summary>
	public string Name { get; set; } = null!;

	/// <summary>
	/// The digital assets associated with the referenced community.
	/// </summary>
	public Dictionary<string, List<DigitalAssetResponse>>? DigitalAssets { get; set; }

}