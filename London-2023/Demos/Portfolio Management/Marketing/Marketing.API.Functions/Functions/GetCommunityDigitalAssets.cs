namespace SLS.Marketing;

public class GetCommunityDigitalAssets
{

	private readonly ILogger _logger;
	private readonly ICommunityDigitalAssetsServices _services;
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public GetCommunityDigitalAssets(
		ICommunityDigitalAssetsServices services,
		JsonSerializerOptions jsonSerializerOptions,
		ILoggerFactory loggerFactory)
	{
		_services = services;
		_jsonSerializerOptions = jsonSerializerOptions;
		_logger = loggerFactory.CreateLogger<GetCommunityDetails>();
	}

	[Function("GetCommunityDigitalAssets")]
	public async Task<HttpResponseData> RunAsync(
		[HttpTrigger(AuthorizationLevel.Function, "get", Route = "communities/{communityNumber}/digital-assets")] HttpRequestData request,
		string communityNumber)
	{

		try
		{

			ArgumentNullException.ThrowIfNull(communityNumber);

			CommunityDigitalAssetsResponse? response = await _services.GetCommunityDetailsAsync(
				communityNumber,
				new GetCommunityDigitalAssetsOptions()
				{
					LanguageCulture = request.GetQueryStringValue("languageCulture") ?? "",
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