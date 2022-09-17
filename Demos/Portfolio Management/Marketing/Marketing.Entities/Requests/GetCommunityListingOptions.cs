namespace SLS.Marketing.Requests;

/// <summary>
/// Options for filtering the GetCommunityListing endpoint.
/// </summary>
public class GetCommunityListingOptions
{

	/// <summary>
	/// The language/culture to use for returning the text values.
	/// If not supplied, the default language/culture for the community will be used.
	/// </summary>
	public string LanguageCulture { get; set; } = string.Empty;

	/// <summary>
	/// Flag indicating whether to only include filter communities in the results.
	/// </summary>
	public bool OnlyIncludeFeatured { get; set; } = false;

	/// <summary>
	/// Flag indicating whether to include community attributes in the response.
	/// </summary>
	public bool IncludeCommunityAttributes { get; set; } = false;

	/// <summary>
	/// Flag indicating whether to include deactivated communities in the response.
	/// If not supplied, deactivated communities will not be returned in the response.
	/// </summary>
	public bool IncludeDeactivedCommunities { get; set; } = false;

}