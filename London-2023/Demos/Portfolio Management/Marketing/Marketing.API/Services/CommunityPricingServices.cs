namespace SLS.Marketing;

public sealed class CommunityPricingServices : ServicesBase, ICommunityPricingServices
{

	public CommunityPricingServices(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task<CommunityPricingResponse?> GetCommunityPricingAsync(
		string communityNumber,
		GetCommunityPricingOptions? options = null)
	{
		ArgumentNullException.ThrowIfNull(communityNumber);
		if (options is null) options = new();
		CachedCommunityPricing? cachedCommunityPricing = await ExecuteCosmosQuery<CachedCommunityPricing>(GenerateSql(communityNumber, options));
		if (cachedCommunityPricing is not null && ((!options.IncludeDeactivatedCommunities && cachedCommunityPricing.RowStatusId == 1) || options.IncludeDeactivatedCommunities))
			return cachedCommunityPricing.Response;
		else
			return default;
	}

	private static string GenerateSql(
		string communityNumber,
		GetCommunityPricingOptions options)
	{
		StringBuilder response = new($"SELECT * FROM c WHERE c.id = '{CachedResponseDiscriminators.CommunityPricing}_{communityNumber}_{options.RoomGrouping}");
		if (!string.IsNullOrWhiteSpace(options.LanguageCulture)) response.Append($"_{options.LanguageCulture}");
		response.Append('\'');
		return response.ToString();
	}

}