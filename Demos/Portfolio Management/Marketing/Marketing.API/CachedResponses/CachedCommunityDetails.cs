namespace SLS.Marketing.CachedResponses;

public class CachedCommunityDetails : BaseCachedResponse
{

	public CachedCommunityDetails() : base(CachedResponseDiscriminators.CommunityDetails, 0) { }

	public CachedCommunityDetails(
		string communityNumber,
		RoomGrouping roomGrouping,
		string languageCulture,
		int rowStatusId) : base(
			CachedResponseDiscriminators.CommunityDetails,
			rowStatusId,
			communityNumber,
			languageCulture,
			roomGrouping.ToString())
	{
		RoomGrouping = roomGrouping.ToString();
		Response = new();
	}

	public string? RoomGrouping { get; set; }

	public CommunityDetailsResponse? Response { get; set; }

}