namespace SLS.Marketing.Responses;

public class DigitalAssetResponse
{

	/// <summary>
	/// The discriminator for the particular image.
	/// </summary>
	public string? Discriminator { get; set; }

	/// <summary>
	/// The name of the digital asset.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// The caption for the digital asset.
	/// </summary>
	public string? Caption { get; set; }

	/// <summary>
	/// The alternative text for the digital asset.
	/// </summary>
	public string? AltText { get; set; }

	/// <summary>
	/// The URL for the digital asset.
	/// </summary>
	public Uri? Url { get; set; }

	/// <summary>
	/// The URl for the digital asset's thumbnail.
	/// </summary>
	public Uri? ThumbnailUrl { get; set; }

	/// <summary>
	/// Flag indicating whether the digital asset is to be featured.
	/// </summary>
	public bool IsFeatured { get; set; }

}