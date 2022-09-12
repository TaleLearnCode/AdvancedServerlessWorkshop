namespace SLS.Marketing.ResponseBuilding.Extensions;

internal static class UriExtensions
{
	public static Uri? ToUri(this string? url)
	{
		if (url is not null)
			return new Uri(url);
		else
			return null;
	}
}