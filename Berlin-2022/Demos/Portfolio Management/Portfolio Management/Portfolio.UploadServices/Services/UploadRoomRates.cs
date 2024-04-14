using Microsoft.EntityFrameworkCore;
using SLS.Porfolio.Repository;
using SLS.Porfolio.Repository.Entiies;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace SLS.Portfolio.UploadServices;

internal sealed class UploadRoomRates : UploadBase
{

	public static async Task ProcessUploadAsync(UploadDirective uploadDirective, string blobContents)
	{
		await new UploadRoomRates().UploadAsync(uploadDirective, blobContents);
	}

	private async Task UploadAsync(UploadDirective uploadDirective, string blobContents)
	{
		List<RoomRateInput>? parsedUpload = ParseUploadedFile(blobContents);
		if (parsedUpload is not null && parsedUpload.Any())
		{
			using PortfolioContext portfolioContext = new();
			Dictionary<string, RoomRate> roomRates = (uploadDirective.Delimiter == "ALL") ? await GetRoomRatesAsync(portfolioContext) : await GetRoomRatesAsync(portfolioContext, uploadDirective.Delimiter);
			foreach (RoomRateInput roomRateInput in parsedUpload)
				if (!string.IsNullOrWhiteSpace(roomRateInput.ExternalId))
					if (roomRates.TryGetValue(roomRateInput.ExternalId, out RoomRate? roomRate) && roomRate is not null)
					{
						await UpdateRoomRateAsync(portfolioContext, roomRate, roomRateInput);
						roomRates.Remove(roomRateInput.ExternalId);
					}
					else
						await CreateRoomRateAsync(portfolioContext, roomRateInput);
			if (uploadDirective.Options?.ToUpperInvariant() == "PURGE") await PurgeRoomRates(portfolioContext, roomRates.Values.ToList());
		}
	}

	private List<RoomRateInput>? ParseUploadedFile(string uploadedFile)
	{
		CsvParser<RoomRateInput> csvParser = new(new(true, ','), new RoomRateMapper());
		List<CsvMappingResult<RoomRateInput>>? parsedResults = csvParser.ReadFromString(_csvReaderOptions, uploadedFile).ToList();
		if (parsedResults.Any())
			return parsedResults.Select(x => x.Result).ToList();
		else
			return default;
	}

	private static async Task<Dictionary<string, RoomRate>> GetRoomRatesAsync(PortfolioContext portfolioContext)
	{
		return PopulateRoomRatesDictionary(await portfolioContext.RoomRates.ToListAsync());
	}

	private static async Task<Dictionary<string, RoomRate>> GetRoomRatesAsync(PortfolioContext portfolioContext, string? communityNumber)
	{
		return PopulateRoomRatesDictionary(
			await portfolioContext.RoomRates
				.Include(x => x.Room)
					.ThenInclude(x => x.Community)
				.Where(x => x.Room.Community.CommunityNumber == communityNumber)
				.ToListAsync());
	}

	private static Dictionary<string, RoomRate> PopulateRoomRatesDictionary(List<RoomRate>? roomRates)
	{
		Dictionary<string, RoomRate> results = new();
		if (roomRates is not null && roomRates.Any())
			foreach (RoomRate roomRate in roomRates)
				if (roomRate.ExternalId is not null)
					results.TryAdd(roomRate.ExternalId, roomRate);
				else
					results.TryAdd(roomRate.RoomRateId.ToString(), roomRate);
		return results;
	}

	private static async Task UpdateRoomRateAsync(PortfolioContext portfolioContext, RoomRate roomRate, RoomRateInput roomRateInput)
	{
		if (roomRateInput.EffectiveDate != roomRate.EffectiveEndDate
			|| roomRateInput.BaseRate != roomRate.BaseRate
			|| roomRateInput.DailyRate != roomRate.DailyRate
			|| roomRateInput.MinimumRate != roomRate.MinimumRate
			|| roomRateInput.MaximumRate != roomRate.MaximumRate
			|| roomRateInput.DiscountedRate != roomRate.DiscountedRate)
		{
			roomRate.EffectiveStartDate = roomRateInput.EffectiveDate;
			roomRate.BaseRate = roomRateInput.BaseRate;
			roomRate.DailyRate = roomRateInput.DailyRate;
			roomRate.MinimumRate = roomRateInput.MinimumRate;
			roomRate.MaximumRate = roomRateInput.MaximumRate;
			roomRate.DiscountedRate = roomRateInput.DiscountedRate;
			portfolioContext.Update(roomRate);
			await portfolioContext.SaveChangesAsync();
		}

	}

	private static async Task CreateRoomRateAsync(PortfolioContext portfolioContext, RoomRateInput roomRateInput)
	{
		await portfolioContext.RoomRates.AddAsync(new()
		{
			EffectiveStartDate = roomRateInput.EffectiveDate,
			BaseRate = roomRateInput.BaseRate,
			DailyRate = roomRateInput.DailyRate,
			MinimumRate = roomRateInput.MinimumRate,
			MaximumRate = roomRateInput.MaximumRate,
			DiscountedRate = roomRateInput.DiscountedRate
		});
		await portfolioContext.SaveChangesAsync();
	}

	private static async Task PurgeRoomRates(PortfolioContext portfolioContext, List<RoomRate> roomRates)
	{
		if (roomRates is not null && roomRates.Any())
		{
			portfolioContext.RemoveRange(roomRates);
			await portfolioContext.SaveChangesAsync();
		}
	}

}