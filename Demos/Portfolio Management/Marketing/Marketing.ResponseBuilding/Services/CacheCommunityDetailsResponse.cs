namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityDetailsResponse : CacheResponseBase, ICacheCommunityDetailsResponse
{

	public CacheCommunityDetailsResponse(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		using PortfolioContext portfolioContext = new PortfolioContext();
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
			CachedResponseDiscriminators.CommunityDetails,
			new CachedCommunityDetails(community.CommunityNumber, roomGrouping, languageCulture, community.RowStatusId)
			{
				Response = new()
				{
					Number = community.CommunityNumber,
					Name = community.CommunityName,
					PhoneNumber = await GetCommunityPhoneNumberAsync(portfolioContext, community.CommunityId),
					PostalAddress = await GetCommunityPostalAddressAsync(portfolioContext, community.CommunityId),
					StartingAtPrice = await GetCommunityStartingAtPriceAsync(portfolioContext, community.CommunityId),
					Pricing = await GetCommunityPricingAsync(portfolioContext, community.CommunityId, roomGrouping),
					DigitalAssets = await GetDigitalAssetsAsync(portfolioContext, community.CommunityId, languageCulture, community.LanguageCultureCode),
					Attributes = await GetCommunityAttributesAsync(portfolioContext, community.CommunityId, languageCulture, community.LanguageCultureCode)
				}
			});
	}

}