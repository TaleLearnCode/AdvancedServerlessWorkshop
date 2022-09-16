namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityPricingResponse : CacheResponseBase, ICacheCommunityPricingResponse
{

	public CacheCommunityPricingResponse(
		PortfolioContext portfolioContext,
		Container cosmosContainer) : base(portfolioContext, cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		Community? community = await GetCommunityAsync(communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
				foreach (RoomGrouping roomGrouping in GetRoomGroupings())
					await BuildAsync(community, roomGrouping, languageCulture);
	}

	private async Task<CacheResponseResult> BuildAsync(
		Community community,
		RoomGrouping roomGrouping,
		string languageCulture)
	{
		return await UpsertCosmosItemAsync(
			CachedResponseDiscriminators.CommunityPricing,
			new CachedCommunityPricing(community.CommunityNumber, roomGrouping, languageCulture, community.RowStatusId)
			{
				Response = new()
				{
					Number = community.CommunityNumber,
					Name = community.CommunityName,
					StartingAtPrice = await GetCommunityStartingAtPriceAsync(community.CommunityId),
					Pricing = await GetCommunityPricingAsync(community.CommunityId, roomGrouping),
				}
			});
	}

}