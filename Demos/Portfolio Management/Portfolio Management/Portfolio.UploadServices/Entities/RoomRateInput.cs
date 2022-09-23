namespace SLS.Portfolio.UploadServices;

public class RoomRateInput
{
	public string? ExternalId { get; set; }
	public DateTime EffectiveDate { get; set; }
	public int BaseRate { get; set; }
	public int DailyRate { get; set; }
	public int MinimumRate { get; set; }
	public int MaximumRate { get; set; }
	public int DiscountedRate { get; set; }
}