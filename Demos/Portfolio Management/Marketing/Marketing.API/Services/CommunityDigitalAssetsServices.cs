namespace SLS.Marketing;

public class CommunityDigitalAssetsServices : ServicesBase, ICommunityDigitalAssetsServices
{

	public CommunityDigitalAssetsServices(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task<CommunityDigitalAssetsResponse?> GetCommunityDetailsAsync(
		string communityNumber,
		GetCommunityDigitalAssetsOptions? options = null)
	{
		ArgumentNullException.ThrowIfNull(communityNumber);
		if (options is null) options = new();
		CachedCommunityDigitalAssets? cachedCommunityDigitalAssets = await ExecuteCosmosQuery<CachedCommunityDigitalAssets>(GenerateSql(communityNumber, options));
		if (cachedCommunityDigitalAssets is not null && ((!options.IncludeDeactivatedCommunities && cachedCommunityDigitalAssets.RowStatusId == 1) || options.IncludeDeactivatedCommunities))
			return cachedCommunityDigitalAssets.Response;
		else
			return default;
	}

	private static string GenerateSql(
		string communityNumber,
		GetCommunityDigitalAssetsOptions options)
	{
		StringBuilder response = new($"SELECT * FROM c WHERE c.id = '{CachedResponseDiscriminators.CommunityDigitalAssets}_{communityNumber}");
		if (!string.IsNullOrWhiteSpace(options.LanguageCulture)) response.Append($"_{options.LanguageCulture}");
		response.Append('\'');
		return response.ToString();
	}

}