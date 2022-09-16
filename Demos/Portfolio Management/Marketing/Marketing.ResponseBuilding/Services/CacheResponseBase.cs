namespace SLS.Marketing.ResponseBuilding;

public abstract class CacheResponseBase
{

	protected readonly PortfolioContext _portfolioContext;
	private readonly Container _cosmosContainer;

	protected CacheResponseBase(
		PortfolioContext portfolioContext,
		Container cosmosContainer)
	{
		_portfolioContext = portfolioContext;
		_cosmosContainer = cosmosContainer;
	}

	protected async Task<CacheResponseResult> UpsertCosmosItemAsync<T>(
		string partitionKey,
		T item) where T : BaseCachedResponse
	{
		try
		{
			if (await CosmosItemExists<T>(item.Id, partitionKey))
				await _cosmosContainer.ReplaceItemAsync(item, item.Id, new PartitionKey(partitionKey));
			else
				await _cosmosContainer.CreateItemAsync(item, new PartitionKey(partitionKey));
		}
		catch (Exception ex)
		{
			return new()
			{
				CosmosId = item.Id,
				Status = "Failed",
				ExceptionDetail = ex.Message
			};
		}
		return new()
		{
			CosmosId = item.Id,
			Status = "Success"
		};
	}

	protected async Task<bool> CosmosItemExists<T>(
		string id,
		string partitionKey)
	{
		try
		{
			await _cosmosContainer.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
			return true;
		}
		catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			return false;
		}
	}

	protected async Task<Community?> GetCommunityAsync(string communityNumber)
	{
		return await _portfolioContext.Communities
			.FirstOrDefaultAsync(x => x.CommunityNumber == communityNumber);
	}

	protected static List<string> GetCommunityLanguageCultures()
	{
		// TODO: Add logic to get the list for the community
		return new() { "en-US", string.Empty };
	}

	protected async Task<Dictionary<string, List<DigitalAssetResponse>>?> GetDigitalAssetsAsync(
		int communityId,
		string languageCulture,
		string? defaultLanguageCulture)
	{
		List<CommunityDigitalAsset>? communityDigitalAssets = await _portfolioContext.CommunityDigitalAssets
			.Include(x => x.DigitalAsset)
				.ThenInclude(x => x.DigitalAssetType)
			.Where(x => x.CommunityId == communityId)
			.ToListAsync();
		if (communityDigitalAssets is not null)
		{
			Dictionary<string, List<DigitalAssetResponse>> response = new();
			foreach (CommunityDigitalAsset communityDigitalAsset in communityDigitalAssets)
			{
				response.TryAdd(communityDigitalAsset.DigitalAsset.DigitalAssetType.Discriminator, new List<DigitalAssetResponse>());
				response[communityDigitalAsset.DigitalAsset.DigitalAssetType.Discriminator].Add(new DigitalAssetResponse()
				{
					Discriminator = communityDigitalAsset.DigitalAsset.Discriminator,
					Name = communityDigitalAsset.DigitalAsset.DigitalAssetName,
					Caption = await GetContentCopyAsync(communityDigitalAsset.DigitalAsset.CaptionId, languageCulture, defaultLanguageCulture),
					AltText = await GetContentCopyAsync(communityDigitalAsset.DigitalAsset.AltTextId, languageCulture, defaultLanguageCulture),
					Url = communityDigitalAsset.DigitalAsset.DigitalAssetUrl.ToUri(),
					ThumbnailUrl = communityDigitalAsset.DigitalAsset.ThumbnailUrl.ToUri(),
					IsFeatured = communityDigitalAsset.IsFeatured
				});
			}
			return response;
		}
		else
			return default;
	}

	protected async Task<string?> GetContentCopyAsync(
		int? contentId,
		string languageCulture,
		string? defaultLanguageCulture)
	{
		if (contentId is not null)
		{
			Content? content = await _portfolioContext.Contents
				.Include(x => x.ContentCopies)
				.FirstOrDefaultAsync(x => x.ContentId == contentId);
			if (content is not null && content.ContentCopies.Any())
			{
				return content.ContentCopies.FirstOrDefault(x => x.LanguageCultureCode == languageCulture)?.CopyText ??
					content.ContentCopies.FirstOrDefault(x => x.LanguageCultureCode == defaultLanguageCulture)?.CopyText ??
					content.ContentCopies.First().CopyText;
			}
			else
				return default;
		}
		return default;
	}

}