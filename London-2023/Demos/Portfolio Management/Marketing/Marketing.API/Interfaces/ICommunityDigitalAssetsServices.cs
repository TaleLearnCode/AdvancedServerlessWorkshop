namespace SLS.Marketing
{
	public interface ICommunityDigitalAssetsServices
	{
		Task<CommunityDigitalAssetsResponse?> GetCommunityDetailsAsync(string communityNumber, GetCommunityDigitalAssetsOptions? options = null);
	}
}