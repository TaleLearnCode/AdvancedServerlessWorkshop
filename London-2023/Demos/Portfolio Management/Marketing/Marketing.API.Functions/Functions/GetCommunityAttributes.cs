namespace SLS.Marketing;

public class GetCommunityAttributes
{

	private readonly ILogger _logger;
	private readonly ICommunityAttributesServices _services;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public GetCommunityAttributes(
		ICommunityAttributesServices services,
		JsonSerializerOptions jsonSerializerOptions,
		ILoggerFactory loggerFactory)
	{
		_services = services;
		_jsonSerializerOptions = jsonSerializerOptions;
		_logger = loggerFactory.CreateLogger<GetCommunityAttributes>();
	}

	[Function("GetCommunityAttributes")]
	public async Task<HttpResponseData> RunAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "communities/{communityNumber}/attributes")] HttpRequestData request,
		string communityNumber)
	{

		try
		{

			ArgumentNullException.ThrowIfNull(communityNumber);

			CommunityAttributesResponse? response = await _services.GetCommunityAttributesAsync(
				communityNumber,
				new GetCommunityAttributesOptions()
				{
					LanguageCulture = request.GetQueryStringValue("languageCulture") ?? "",
					IncludeDeactivedCommunities = request.GetBooleanQueryStringValue("includeDeactivatedCommunities", false)
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