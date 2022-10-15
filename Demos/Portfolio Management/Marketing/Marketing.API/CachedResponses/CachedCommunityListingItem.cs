namespace SLS.Marketing.CachedResponses;

public class CachedCommunityListingItem : BaseCachedResponse
{

	public CachedCommunityListingItem() : base(CachedResponseDiscriminators.CommunityListingItem, 0) { }

	public CachedCommunityListingItem(
		string communityNumber,
		string languageCulture,
		int rowStatusId,
		bool includeCommunityAttributes) : base(
			CachedResponseDiscriminators.CommunityListingItem,
			rowStatusId,
			communityNumber,
			languageCulture,
			BuildConstructorOptions(includeCommunityAttributes))
	{
		Response = new();
	}

	public CommunityListingItemResponse? Response { get; set; }

	public bool IsFetured { get; set; } = false;

	public bool IncludeAttributes { get; set; } = false;

	public static string? BuildConstructorOptions(bool includeCommunityAttributes)
	{
		if (includeCommunityAttributes)
			return "Attributes";
		else
			return default;
	}

}