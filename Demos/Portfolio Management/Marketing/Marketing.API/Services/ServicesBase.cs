namespace SLS.Marketing;

public abstract class ServicesBase
{

	private readonly Container _cosmosContainer;

	protected ServicesBase(Container cosmosContainer)
	{
		_cosmosContainer = cosmosContainer;
	}

	protected async Task<T?> ExecuteCosmosQuery<T>(string query)
	{
		using FeedIterator<T> feedIterator = _cosmosContainer.GetItemQueryIterator<T>(query);
		T? response = default;
		while (feedIterator.HasMoreResults)
		{
			FeedResponse<T> curentResultSet = await feedIterator.ReadNextAsync();
			foreach (T item in curentResultSet)
			{
				response = item;
			}
		}
		return response;
	}

}