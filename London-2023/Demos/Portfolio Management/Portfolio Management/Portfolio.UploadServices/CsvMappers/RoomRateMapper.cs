using TinyCsvParser.Mapping;

namespace SLS.Portfolio.UploadServices;

public class RoomRateMapper : CsvMapping<RoomRateInput>
{
	public RoomRateMapper() : base()
	{
		MapProperty(0, x => x.ExternalId);
		MapProperty(1, x => x.EffectiveDate);
		MapProperty(2, x => x.BaseRate);
		MapProperty(3, x => x.DailyRate);
		MapProperty(4, x => x.MinimumRate);
		MapProperty(5, x => x.MaximumRate);
		MapProperty(6, x => x.DiscountedRate);
	}
}