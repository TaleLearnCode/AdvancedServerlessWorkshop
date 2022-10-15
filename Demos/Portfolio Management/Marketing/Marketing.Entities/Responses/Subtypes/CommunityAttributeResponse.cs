namespace SLS.Marketing.Responses;

public class CommunityAttributeResponse
{

	/// <summary>
	/// The label for the community attribute.
	/// </summary>
	public string Label { get; set; } = null!;

	/// <summary>
	/// The URL of the community attribute's icon.
	/// </summary>
	public Uri? IconUrl { get; set; }

	/// <summary>
	/// The alternative text for the community attribute's icon.
	/// </summary>
	public string? AltText { get; set; }

}