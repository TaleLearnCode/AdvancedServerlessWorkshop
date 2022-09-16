namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityAttributesResponse : CacheResponseBase, ICacheCommunityAttributesResponse
{

	public CacheCommunityAttributesResponse(
		PortfolioContext portfolioContext,
		Container cosmosContainer) : base(portfolioContext, cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		Community? community = await GetCommunityAsync(communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
				await BuildAsync(community, languageCulture);
	}

	private async Task<CacheResponseResult> BuildAsync(
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
					Attributes = await GetCommunityAttributesAsync(community.CommunityId, languageCulture, community.LanguageCultureCode)
				}
			});
	}

}