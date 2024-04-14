namespace SLS.Marketing;

public class GetCommunityListing
{

	private readonly ILogger _logger;
	private readonly ICommunityListingServices _services;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public GetCommunityListing(
		ICommunityListingServices services,
		JsonSerializerOptions jsonSerializerOptions,
		ILoggerFactory loggerFactory)
	{
		_services = services;
		_jsonSerializerOptions = jsonSerializerOptions;
		_logger = loggerFactory.CreateLogger<GetCommunityListing>();
	}

	[Function("GetCommunityListing")]
	public async Task<HttpResponseData> RunAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "communities")] HttpRequestData request,
		string communityNumber)
	{

		try
		{

			CommunityListingResponse? response = await _services.GetCommunityListingAsync(
				new GetCommunityListingOptions()
				{
					LanguageCulture = request.GetQueryStringValue("languageCulture") ?? "",
					OnlyIncludeFeatured = request.GetBooleanQueryStringValue("onlyFeatured", false),
					IncludeCommunityAttributes = request.GetBooleanQueryStringValue("includeAttributes", false),
					IncludeDeactivedCommunities = request.GetBooleanQueryStringValue("includeDeactivatedCommunities", false),
					PageSize = request.GetIntegerQueryStringValue("pageSize", 0),
					PageNumber = request.GetIntegerQueryStringValue("pageNumber", 1)
				});

			return await request.CreateResponseAsync(response, _jsonSerializerOptions);

		}
		catch (Exception ex) when (ex is ArgumentNullException)
		{
			return request.CreateBadRequestResponse(ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("Unexpected exception: {ExceptionMesage}", ex.Message);
			return request.CreateErrorResponse(ex);
		}

	}

}