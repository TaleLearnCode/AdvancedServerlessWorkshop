namespace SLS.Marketing
{
	public interface ICommunityListingServices
	{
		Task<CommunityListingResponse> GetCommunityListingAsync(GetCommunityListingOptions? options = null);
	}
}