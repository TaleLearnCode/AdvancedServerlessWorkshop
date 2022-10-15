namespace SLS.Marketing
{
	public interface ICommunityDetailsServices
	{
		Task<CommunityDetailsResponse?> GetCommunityDetailsAsync(string communityNumber, GetCommunityDetailsOptions? options = null);
	}
}