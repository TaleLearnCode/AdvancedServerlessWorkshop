namespace SLS.Marketing.ResponseBuilding;

public interface ICacheCommunityDigitalAssetsResponse
{
	Task BuildAsync(string communityNumber);
}