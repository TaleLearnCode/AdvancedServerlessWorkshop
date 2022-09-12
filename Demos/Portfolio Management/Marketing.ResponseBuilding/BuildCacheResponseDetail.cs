namespace SLS.Marketing.ResponseBuilding;

public class BuildCacheResponseDetail
{
	public string CosmosId { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string? ExceptionDetail { get; set; }
}