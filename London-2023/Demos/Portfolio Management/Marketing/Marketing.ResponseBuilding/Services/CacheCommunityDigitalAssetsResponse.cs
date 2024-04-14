namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityDigitalAssetsResponse : CacheResponseBase, ICacheCommunityDigitalAssetsResponse
{

	public CacheCommunityDigitalAssetsResponse(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		using PortfolioContext portfolioContext = new PortfolioContext();
		Community? community = await GetCommunityAsync(portfolioContext, communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
				await BuildAsync(portfolioContext, community, languageCulture);
	}

	private async Task<CacheResponseResult> BuildAsync(
		PortfolioContext portfolioContext,
		Community community,
		string languageCulture)
	{
		return await UpsertCosmosItemAsync(
			CachedResponseDiscriminators.CommunityDigitalAssets,
			new CachedCommunityDigitalAssets(community.CommunityNumber, languageCulture, community.RowStatusId)
			{
				Response = new()
				{
					Number = community.CommunityNumber,
					Name = community.CommunityName,
					DigitalAssets = await GetDigitalAssetsAsync(portfolioContext, community.CommunityId, languageCulture, community.LanguageCultureCode)
				}
			});
	}

}