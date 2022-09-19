namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityListingResponse : CacheResponseBase, ICacheCommunityListingResponse
{

	public CacheCommunityListingResponse(
		PortfolioContext portfolioContext,
		Container cosmosContainer) : base(portfolioContext, cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		Community? community = await GetCommunityAsync(communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
			{
				await BuildAsync(community, await GetCommunityPostalAddressAsync(community.CommunityId), languageCulture, true);
				await BuildAsync(community, await GetCommunityPostalAddressAsync(community.CommunityId), languageCulture, false);
			}
	}

	private async Task<CacheResponseResult> BuildAsync(
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
					PhoneNumber = await GetCommunityPhoneNumberAsync(community.CommunityId),
					CommunityPhotoUrl = await GetCommunityPhotoUrl(community.CommunityId),
					StartingAtPrice = await GetCommunityStartingAtPriceAsync(community.CommunityId),
					IsFeatured = community.IsFeatured,
					CareTypes = await GetCareTypeItemsAsync(community.CommunityId),
					Attributes = (includeCommunityAttributes) ? await GetCommunityAttributesAsync(community.CommunityId, languageCulture, community.LanguageCultureCode) : null
				},
				IsFetured = community.IsFeatured,
				IncludeAttributes = includeCommunityAttributes
			});
	}

	private async Task<Uri?> GetCommunityPhotoUrl(int communityId)
	{
		Community? community = await _portfolioContext.Communities
			.Include(x => x.ProfileImage)
			.FirstOrDefaultAsync(x => x.CommunityId == communityId);
		return community?.ProfileImage?.DigitalAssetUrl.ToUri();
	}

	private async Task<List<CareTypeItemResponse>?> GetCareTypeItemsAsync(int communityId)
	{
		List<CommunityCareType>? communityCareTypes = await _portfolioContext.CommunityCareTypes
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