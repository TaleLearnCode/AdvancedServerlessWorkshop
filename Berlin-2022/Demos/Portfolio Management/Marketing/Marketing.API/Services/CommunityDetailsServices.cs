namespace SLS.Marketing;

public sealed class CommunityDetailsServices : ServicesBase, ICommunityDetailsServices
{

	public CommunityDetailsServices(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task<CommunityDetailsResponse?> GetCommunityDetailsAsync(
		string communityNumber,
		GetCommunityDetailsOptions? options = null)
	{
		ArgumentNullException.ThrowIfNull(communityNumber);
		if (options is null) options = new();
		CachedCommunityDetails? cachedCommunityDetails = await ExecuteCosmosQuery<CachedCommunityDetails>(GenerateSql(communityNumber, options));
		if (cachedCommunityDetails is not null && ((!options.IncludeDeactivedCommunities && cachedCommunityDetails.RowStatusId == 1) || options.IncludeDeactivedCommunities))
			return cachedCommunityDetails.Response;
		else
			return default;
	}

	private static string GenerateSql(
		string communityNumber,
		GetCommunityDetailsOptions options)
	{
		StringBuilder response = new($"SELECT * FROM c WHERE c.id = '{CachedResponseDiscriminators.CommunityDetails}_{communityNumber}_{options.RoomGrouping}");
		if (!string.IsNullOrWhiteSpace(options.LanguageCulture)) response.Append($"_{options.LanguageCulture}");
		response.Append('\'');
		return response.ToString();
	}

}