namespace SLS.Marketing.CachedResponses;

public class CachedCommunityDetails : BaseCachedResponse
{

	public CachedCommunityDetails() : base(CachedResponseDiscriminators.CommunityDetails) { }

	public CachedCommunityDetails(
		string communityNumber,
		RoomGrouping roomGrouping,
		string languageCultureId,
		int rowStatusId) : base(
			CachedResponseDiscriminators.CommunityDetails,
			communityNumber,
			GetOptionParameter(roomGrouping, languageCultureId))
	{
		RowStatusId = rowStatusId;
		RoomGrouping = roomGrouping.ToString();
		LanguageCulture = languageCultureId;
		Response = new();
	}

	public int RowStatusId { get; set; }

	public string? LanguageCulture { get; set; }

	public string? RoomGrouping { get; set; }

	public CommunityDetailsResponse? Response { get; set; }

	private static string GetOptionParameter(
		RoomGrouping roomGrouping,
		string languageCultureId)
	{
		StringBuilder response = new(roomGrouping.ToString());
		if (!string.IsNullOrWhiteSpace(languageCultureId)) response.Append($"_{languageCultureId}");
		return response.ToString();
	}

}