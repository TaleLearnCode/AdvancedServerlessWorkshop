namespace SLS.Marketing;

public sealed class CommunityListingServices : ServicesBase, ICommunityListingServices
{

	public CommunityListingServices(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task<CommunityListingResponse> GetCommunityListingAsync(
		GetCommunityListingOptions? options = null)
	{
		if (options is null) options = new();
		List<CachedCommunityListingItem> communities = await ExecuteCosmosQueryAsync<CachedCommunityListingItem>(GenerateSql(options));
		return new()
		{
			TotalCommunities = communities.Count,
			CommunitiesReturned = communities.Count,
			PageSize = 0,
			PageCount = 1,
			PageNumber = 1,
			Communities = ExtractCommunityListingItems(communities)
		};
	}

	private static string GenerateSql(
		GetCommunityListingOptions options)
	{
		StringBuilder response = new($"SELECT * FROM c WHERE c.Discriminator = '{CachedResponseDiscriminators.CommunityListingItem}'");
		response.Append($" AND c.LanguageCulture = '{options.LanguageCulture}'");
		response.Append($" AND c.IncludeAttributes = {options.IncludeCommunityAttributes.ToString().ToLowerInvariant()}");
		if (options.OnlyIncludeFeatured) response.Append(" AND c.IsFeatured = true");
		if (!options.IncludeDeactivedCommunities) response.Append(" AND c.RowStatusId = 1");
		return response.ToString();
	}

	private static List<CommunityListingItemResponse> ExtractCommunityListingItems(List<CachedCommunityListingItem> cachedCommunityListingItems)
	{
		List<CommunityListingItemResponse> response = new();
		foreach (CommunityListingItemResponse? itemResponse in cachedCommunityListingItems.Select(x => x.Response))
			if (itemResponse is not null)
				response.Add(itemResponse);
		return response;
	}
}