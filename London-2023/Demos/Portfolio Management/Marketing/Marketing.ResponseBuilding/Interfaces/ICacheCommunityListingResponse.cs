namespace SLS.Marketing.ResponseBuilding
{
	public interface ICacheCommunityListingResponse
	{
		Task BuildAsync(string communityNumber);
	}
}