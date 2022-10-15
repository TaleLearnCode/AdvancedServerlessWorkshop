namespace SLS.Marketing.ResponseBuilding.Entities;

public class CacheResponseResult
{
	public string CosmosId { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string? ExceptionDetail { get; set; }
}