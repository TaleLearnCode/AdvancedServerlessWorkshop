namespace SLS.Marketing
{
	public interface ICommunityPricingServices
	{
		Task<CommunityPricingResponse?> GetCommunityPricingAsync(string communityNumber, GetCommunityPricingOptions? options = null);
	}
}