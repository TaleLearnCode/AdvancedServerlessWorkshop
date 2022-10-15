namespace SLS.Marketing.Requests;

/// <summary>
/// Options for filtering the GetCommunityDetails endpoint.
/// </summary>
public class GetCommunityDetailsOptions
{

	/// <summary>
	/// The room grouping to use for displaying room availability and pricing.
	/// The default value is <see cref="RoomGrouping.RoomStyle"/>.
	/// </summary>
	public RoomGrouping RoomGrouping { get; set; } = RoomGrouping.RoomStyle;

	/// <summary>
	/// The language/culture to use for returning the text values.
	/// If not supplied, the default language/culture for the community will be used.
	/// </summary>
	public string LanguageCulture { get; set; } = string.Empty;

	/// <summary>
	/// Flag indicating whether to include deactivated communities in the response.
	/// If not supplied, deactivated communities will not be returned in the response.
	/// </summary>
	public bool IncludeDeactivedCommunities { get; set; } = false;

}