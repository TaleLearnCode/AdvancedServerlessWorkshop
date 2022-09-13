namespace SLS.Azure.Functions;

/// <summary>
/// Interface for types representing a request for an
/// Azure Function that will return paged results.
/// </summary>
public interface IPagedRequest
{

	/// <summary>
	/// Gets or sets the index of the page of results to be returned.
	/// </summary>
	/// <value>
	/// A <c>int</c> representing the page index to be returned.
	/// </value>
	int? PageIndex { get; set; }

	/// <summary>
	/// Gets or sets the size of the page of results to be returned.
	/// </summary>
	/// <value>
	/// A <c>int</c> representing the results page size.
	/// </value>
	int? PageSize { get; set; }

}