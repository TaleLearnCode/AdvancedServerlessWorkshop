namespace SLS.Marketing.CachedResponses;

public abstract class BaseCachedResponse
{

	protected BaseCachedResponse(
		string discriminator,
		string? objectId = null,
		string? option = null,
		string? languageCultureId = null)
	{
		Discriminator = discriminator;
		Id = GenerateId(discriminator, objectId, option);
	}

	/// <summary>
	/// The Cosmos identifier for the cached response.
	/// </summary>
	[JsonProperty(PropertyName = "id")]
	public string Id { get; set; }

	/// <summary>
	/// The discriminator defining what type of response is being cached.
	/// </summary>
	public string Discriminator { get; set; }

	private static string GenerateId(
		string discriminator,
		string? objectId = null,
		string? option = null)
	{
		StringBuilder response = new(discriminator);
		if (!string.IsNullOrWhiteSpace(objectId)) response.Append($"_{objectId}");
		if (!string.IsNullOrWhiteSpace(objectId)) response.Append($"_{option}");
		return response.ToString();
	}

}