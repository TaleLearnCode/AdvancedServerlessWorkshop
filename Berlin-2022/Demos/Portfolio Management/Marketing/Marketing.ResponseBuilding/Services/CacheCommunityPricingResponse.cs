namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityPricingResponse : CacheResponseBase, ICacheCommunityPricingResponse
{

	public CacheCommunityPricingResponse(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		using PortfolioContext portfolioContext = new();
		Community? community = await GetCommunityAsync(portfolioContext, communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
				foreach (RoomGrouping roomGrouping in GetRoomGroupings())
					await BuildAsync(portfolioContext, community, roomGrouping, languageCulture);
	}

	private async Task<CacheResponseResult> BuildAsync(
		PortfolioContext portfolioContext,
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
					StartingAtPrice = await GetCommunityStartingAtPriceAsync(portfolioContext, community.CommunityId),
					Pricing = await GetCommunityPricingAsync(portfolioContext, community.CommunityId, roomGrouping),
				}
			});
	}

}