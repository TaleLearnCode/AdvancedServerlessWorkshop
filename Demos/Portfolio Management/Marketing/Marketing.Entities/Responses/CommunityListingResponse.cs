namespace SLS.Marketing.Responses;

/// <summary>
/// Represents the results for the GetCommunityListing endpoint.
/// </summary>
public class CommunityListingResponse
{

	/// <summary>
	/// The total number of communities matching the filter criteria.
	/// </summary>
	public int TotalCommunities { get; set; }

	/// <summary>
	/// The number of communities returned in the response
	/// </summary>
	public int CommunitiesReturned { get; set; }

	/// <summary>
	/// The size of the page of results in the response.
	/// </summary>
	public int PageSize { get; set; }

	/// <summary>
	/// The number of paged results available.
	/// </summary>
	public int PageCount { get; set; }

	/// <summary>
	/// The page number returned.
	/// </summary>
	public int PageNumber { get; set; }

	/// <summary>
	/// The listing of communities matching the specified filter.
	/// </summary>
	public List<CommunityListingItemResponse>? Communities { get; set; }

}