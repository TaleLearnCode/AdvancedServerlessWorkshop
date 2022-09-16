namespace SLS.Marketing.CachedResponses;

public class CachedCommunityPricing : BaseCachedResponse
{

	public CachedCommunityPricing() : base(CachedResponseDiscriminators.CommunityDetails, 0) { }

	public CachedCommunityPricing(
		string communityNumber,
		RoomGrouping roomGrouping,
		string languageCulture,
		int rowStatusId) : base(
			CachedResponseDiscriminators.CommunityPricing,
			rowStatusId,
			communityNumber,
			languageCulture,
			roomGrouping.ToString())
	{
		Response = new();
	}

	public string? RoomGrouping { get; set; }

	public CommunityPricingResponse? Response { get; set; }

}