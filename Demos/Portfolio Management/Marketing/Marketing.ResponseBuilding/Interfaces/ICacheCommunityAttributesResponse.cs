namespace SLS.Marketing.ResponseBuilding
{
	public interface ICacheCommunityAttributesResponse
	{
		Task BuildAsync(string communityNumber);
	}
}