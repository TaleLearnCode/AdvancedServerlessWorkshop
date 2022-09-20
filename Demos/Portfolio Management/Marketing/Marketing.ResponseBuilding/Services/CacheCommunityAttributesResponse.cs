namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityAttributesResponse : CacheResponseBase2, ICacheCommunityAttributesResponse
{

	public CacheCommunityAttributesResponse(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		using PortfolioContext portfolioContext = new();
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
			CachedResponseDiscriminators.CommunityAttributes,
			new CachedCommunityAttributes(community.CommunityNumber, languageCulture, community.RowStatusId)
			{
				Response = new()
				{
					Number = community.CommunityNumber,
					Name = community.CommunityName,
					Attributes = await GetCommunityAttributesAsync(portfolioContext, community.CommunityId, languageCulture, community.LanguageCultureCode)
				}
			});
	}

}