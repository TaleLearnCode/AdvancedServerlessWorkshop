namespace SLS.Marketing.ResponseBuilding
{
	public interface ICacheCommunityDetailsResponse
	{
		Task BuildAsync(string communityNumber);
	}
}