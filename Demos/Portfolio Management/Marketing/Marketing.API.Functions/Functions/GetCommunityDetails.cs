namespace SLS.Marketing;

public class GetCommunityDetails
{

	private readonly ILogger _logger;
	private readonly ICommunityDetailsServices _services;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public GetCommunityDetails(
		ICommunityDetailsServices services,
		JsonSerializerOptions jsonSerializerOptions,
		ILoggerFactory loggerFactory)
	{
		_services = services;
		_jsonSerializerOptions = jsonSerializerOptions;
		_logger = loggerFactory.CreateLogger<GetCommunityDetails>();
	}

	[Function("GetCommunityDetails")]
	public async Task<HttpResponseData> RunAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "communities/{communityNumber}")] HttpRequestData request,
		string communityNumber)
	{

		try
		{

			ArgumentNullException.ThrowIfNull(communityNumber);

			CommunityDetailsResponse? response = await _services.GetCommunityDetailsAsync(
				communityNumber,
				new GetCommunityDetailsOptions()
				{
					RoomGrouping = FunctionHelper.GetRoomGrouping(request),
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