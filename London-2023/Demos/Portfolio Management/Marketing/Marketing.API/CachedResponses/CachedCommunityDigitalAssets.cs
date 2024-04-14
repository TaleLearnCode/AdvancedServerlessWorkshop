namespace SLS.Marketing.CachedResponses;

public class CachedCommunityDigitalAssets : BaseCachedResponse
{

	public CachedCommunityDigitalAssets() : base(CachedResponseDiscriminators.CommunityDigitalAssets, 0) { }

	public CachedCommunityDigitalAssets(
		string communityNumber,
		string languageCultureId,
		int rowStatusId) : base(
			CachedResponseDiscriminators.CommunityDigitalAssets,
			rowStatusId,
			communityNumber,
			languageCultureId)
	{
		Response = new();
	}

	public CommunityDigitalAssetsResponse? Response { get; set; }

}