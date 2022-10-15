namespace SLS.Marketing.CachedResponses;

public class CachedCommunityAttributes : BaseCachedResponse
{

	public CachedCommunityAttributes() : base(CachedResponseDiscriminators.CommunityDetails, 0) { }

	public CachedCommunityAttributes(
		string communityNumber,
		string languageCulture,
		int rowStatusId) : base(
			CachedResponseDiscriminators.CommunityAttributes,
			rowStatusId,
			communityNumber,
			languageCulture)
	{
		Response = new();
	}

	public CommunityAttributesResponse? Response { get; set; }

}