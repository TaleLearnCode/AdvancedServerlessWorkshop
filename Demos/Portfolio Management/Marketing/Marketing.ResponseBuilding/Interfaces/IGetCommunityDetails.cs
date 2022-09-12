namespace SLS.Marketing.ResponseBuilding
{
	public interface IGetCommunityDetails
	{
		Task BuildAsync(string communityNumber);
	}
}