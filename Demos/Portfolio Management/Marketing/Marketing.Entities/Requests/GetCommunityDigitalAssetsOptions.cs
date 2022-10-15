namespace SLS.Marketing.Requests;

/// <summary>
/// Options used when making requests to GetCommunityDigitalAssets endpoint.
/// </summary>
public class GetCommunityDigitalAssetsOptions
{

	/// <summary>
	/// The language/culture to use for returning the text values.
	/// If not supplied, the default language/culture for the community will be used.
	/// </summary>
	public string LanguageCulture { get; set; } = string.Empty;

	/// <summary>
	/// Flag indicating whether to include deactivated communities in the response.
	/// If not supplied, deactivated communities will not be returned in the response.
	/// </summary>
	public bool IncludeDeactivatedCommunities { get; set; } = false;

}