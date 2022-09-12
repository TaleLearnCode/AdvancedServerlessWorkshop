namespace SLS.Marketing.ResponseBuilding;

public abstract class ResponseBuildingBase
{

	protected readonly PortfolioContext _portfolioContext;
	private readonly Container _cosmosContainer;

	protected ResponseBuildingBase(
		PortfolioContext portfolioContext,
		Container cosmosContainer)
	{
		_portfolioContext = portfolioContext;
		_cosmosContainer = cosmosContainer;
	}

	protected async Task<BuildCacheResponseDetail> UpsertCosmosItemAsync<T>(
		string partitionKey,
		T item) where T : BaseCachedResponse
	{
		try
		{
			if (await CosmosItemExists<T>(item.Id, partitionKey))
				await _cosmosContainer.ReplaceItemAsync<T>(item, item.Id, new PartitionKey(partitionKey));
			else
				await _cosmosContainer.CreateItemAsync<T>(item, new PartitionKey(partitionKey));
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


}