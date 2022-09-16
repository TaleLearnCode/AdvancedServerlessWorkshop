namespace SLS.Marketing;

public class GetCommunityPricing
{

	private readonly ILogger _logger;
	private readonly ICommunityPricingServices _services;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public GetCommunityPricing(
		ICommunityPricingServices services,
		JsonSerializerOptions jsonSerializerOptions,
		ILoggerFactory loggerFactory)
	{
		_services = services;
		_jsonSerializerOptions = jsonSerializerOptions;
		_logger = loggerFactory.CreateLogger<GetCommunityPricing>();
	}

	[Function("GetCommunityPricing")]
	public async Task<HttpResponseData> RunAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "communities/{communityNumber}/pricing")] HttpRequestData request,
		string communityNumber)
	{

		try
		{

			ArgumentNullException.ThrowIfNull(communityNumber);

			CommunityPricingResponse? response = await _services.GetCommunityPricingAsync(
				communityNumber,
				new GetCommunityPricingOptions()
				{
					RoomGrouping = FunctionHelper.GetRoomGrouping(request),
					LanguageCulture = request.GetQueryStringValue("languageCulture") ?? "",
					IncludeDeactivatedCommunities = request.GetBooleanQueryStringValue("includeDeactivatedCommunities", false)
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