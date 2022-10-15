namespace SLS.Marketing
{
	public interface ICommunityAttributesServices
	{
		Task<CommunityAttributesResponse?> GetCommunityAttributesAsync(string communityNumber, GetCommunityAttributesOptions? options = null);
	}
}