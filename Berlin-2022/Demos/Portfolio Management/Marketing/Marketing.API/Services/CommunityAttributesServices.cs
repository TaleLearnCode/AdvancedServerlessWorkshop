namespace SLS.Marketing;

public class CommunityAttributesServices : ServicesBase, ICommunityAttributesServices
{

	public CommunityAttributesServices(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task<CommunityAttributesResponse?> GetCommunityAttributesAsync(
		string communityNumber,
		GetCommunityAttributesOptions? options = null)
	{
		ArgumentNullException.ThrowIfNull(communityNumber);
		if (options is null) options = new();
		CachedCommunityAttributes? cachedCommunityAttributes = await ExecuteCosmosQuery<CachedCommunityAttributes>(GenerateSql(communityNumber, options));
		if (cachedCommunityAttributes is not null && ((!options.IncludeDeactivedCommunities && cachedCommunityAttributes.RowStatusId == 1) || options.IncludeDeactivedCommunities))
			return cachedCommunityAttributes.Response;
		else
			return default;
	}

	private static string GenerateSql(
		string communityNumber,
		GetCommunityAttributesOptions options)
	{
		StringBuilder response = new($"SELECT * FROM c WHERE c.id = '{CachedResponseDiscriminators.CommunityAttributes}_{communityNumber}");
		if (!string.IsNullOrWhiteSpace(options.LanguageCulture)) response.Append($"_{options.LanguageCulture}");
		response.Append('\'');
		return response.ToString();
	}


}