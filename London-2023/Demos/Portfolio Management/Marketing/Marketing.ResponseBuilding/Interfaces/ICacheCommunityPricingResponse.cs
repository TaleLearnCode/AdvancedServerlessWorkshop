namespace SLS.Marketing.ResponseBuilding
{
	public interface ICacheCommunityPricingResponse
	{
		Task BuildAsync(string communityNumber);
	}
}