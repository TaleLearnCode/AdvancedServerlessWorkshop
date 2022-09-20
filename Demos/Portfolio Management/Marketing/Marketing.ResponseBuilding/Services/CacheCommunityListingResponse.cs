namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityListingResponse : CacheResponseBase, ICacheCommunityListingResponse
{

	public CacheCommunityListingResponse(Container cosmosContainer) : base(cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		using PortfolioContext portfolioContext = new PortfolioContext();
		Community? community = await GetCommunityAsync(portfolioContext, communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
			{
				await BuildAsync(portfolioContext, community, await GetCommunityPostalAddressAsync(portfolioContext, community.CommunityId), languageCulture, true);
				await BuildAsync(portfolioContext, community, await GetCommunityPostalAddressAsync(portfolioContext, community.CommunityId), languageCulture, false);
			}
	}

	private async Task<CacheResponseResult> BuildAsync(
		PortfolioContext portfolioContext,
		Community community,
		PostalAddressResponse? postalAddress,
		string languageCulture,
		bool includeCommunityAttributes)
	{
		return await UpsertCosmosItemAsync(
			CachedResponseDiscriminators.CommunityListingItem,
			new CachedCommunityListingItem(community.CommunityNumber, languageCulture, community.RowStatusId, includeCommunityAttributes)
			{
				Response = new()
				{
					Number = community.CommunityNumber,
					Name = community.CommunityName,
					StreetAddress = postalAddress?.StreetAddress1,
					City = postalAddress?.City,
					CountryDivision = postalAddress?.CountryDivision,
					Country = postalAddress?.Country,
					PostalCode = postalAddress?.PostalCode,
					Longitude = community.Longitude,
					Latitude = community.Latitude,
					PhoneNumber = await GetCommunityPhoneNumberAsync(portfolioContext, community.CommunityId),
					CommunityPhotoUrl = await GetCommunityPhotoUrl(portfolioContext, community.CommunityId),
					StartingAtPrice = await GetCommunityStartingAtPriceAsync(portfolioContext, community.CommunityId),
					IsFeatured = community.IsFeatured,
					CareTypes = await GetCareTypeItemsAsync(portfolioContext, community.CommunityId),
					Attributes = (includeCommunityAttributes) ? await GetCommunityAttributesAsync(portfolioContext, community.CommunityId, languageCulture, community.LanguageCultureCode) : null
				},
				IsFetured = community.IsFeatured,
				IncludeAttributes = includeCommunityAttributes
			});
	}

	private async Task<Uri?> GetCommunityPhotoUrl(PortfolioContext portfolioContext, int communityId)
	{
		Community? community = await portfolioContext.Communities
			.Include(x => x.ProfileImage)
			.FirstOrDefaultAsync(x => x.CommunityId == communityId);
		return community?.ProfileImage?.DigitalAssetUrl.ToUri();
	}

	private async Task<List<CareTypeItemResponse>?> GetCareTypeItemsAsync(PortfolioContext portfolioContext, int communityId)
	{
		List<CommunityCareType>? communityCareTypes = await portfolioContext.CommunityCareTypes
			.Include(x => x.CareType)
			.Where(x => x.CommunityId == communityId)
			.ToListAsync();
		if (communityCareTypes.Any())
		{
			List<CareTypeItemResponse> response = new();
			foreach (CareType careType in communityCareTypes.Select(x => x.CareType))
				response.Add(new()
				{
					Code = careType.CareTypeCode,
					Name = careType.CareTypeName
				});
			return response;
		}
		return default;
	}

}