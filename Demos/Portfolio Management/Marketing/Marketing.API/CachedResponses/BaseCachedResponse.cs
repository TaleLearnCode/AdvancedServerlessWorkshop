namespace SLS.Marketing.CachedResponses;

public abstract class BaseCachedResponse
{

	protected BaseCachedResponse(
		string discriminator,
		int rowStatusId,
		string? objectId = null,
		string? languageCulture = null,
		string? option = null)
	{
		Discriminator = discriminator;
		RowStatusId = rowStatusId;
		LanguageCulture = languageCulture;
		Id = GenerateId(discriminator, objectId, option, languageCulture);
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

	public int RowStatusId { get; set; }

	public string? LanguageCulture { get; set; }

	private static string GenerateId(
		string discriminator,
		string? objectId = null,
		string? option = null,
		string? languageCulture = null)
	{
		StringBuilder response = new(discriminator);
		if (!string.IsNullOrWhiteSpace(objectId)) response.Append($"_{objectId}");
		if (!string.IsNullOrWhiteSpace(option)) response.Append($"_{option}");
		if (!string.IsNullOrWhiteSpace(languageCulture)) response.Append($"_{languageCulture}");
		return response.ToString();
	}

}