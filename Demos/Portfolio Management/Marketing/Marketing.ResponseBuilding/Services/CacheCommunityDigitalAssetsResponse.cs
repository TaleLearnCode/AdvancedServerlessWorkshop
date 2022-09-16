namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityDigitalAssetsResponse : CacheResponseBase, ICacheCommunityDigitalAssetsResponse
{

	public CacheCommunityDigitalAssetsResponse(
		PortfolioContext portfolioContext,
		Container cosmosContainer) : base(portfolioContext, cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		Community? community = await GetCommunityAsync(communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
				await BuildAsync(community, languageCulture);
	}

	private async Task<CacheResponseResult> BuildAsync(Community community, string languageCulture)
	{
		return await UpsertCosmosItemAsync(
			CachedResponseDiscriminators.CommunityDigitalAssets,
			new CachedCommunityDigitalAssets(community.CommunityNumber, languageCulture, community.RowStatusId)
			{
				Response = new()
				{
					Number = community.CommunityNumber,
					Name = community.CommunityName,
					DigitalAssets = await GetDigitalAssetsAsync(community.CommunityId, languageCulture, community.LanguageCultureCode)
				}
			});
	}

}