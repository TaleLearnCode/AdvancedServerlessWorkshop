namespace SLS.Marketing.ResponseBuilding;

public class CacheCommunityDetailsResponse : CacheResponseBase, ICacheCommunityDetailsResponse
{

	public CacheCommunityDetailsResponse(
		PortfolioContext portfolioContext,
		Container cosmosContainer) : base(portfolioContext, cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		Community? community = await GetCommunityAsync(communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
				foreach (RoomGrouping roomGrouping in GetRoomGroupings())
					await BuildAsync(community, roomGrouping, languageCulture);
	}

	private async Task<CacheResponseResult> BuildAsync(
		Community community,
		RoomGrouping roomGrouping,
		string languageCulture)
	{
		return await UpsertCosmosItemAsync(
			CachedResponseDiscriminators.CommunityDetails,
			new CachedCommunityDetails(community.CommunityNumber, roomGrouping, languageCulture, community.RowStatusId)
			{
				Response = new()
				{
					Number = community.CommunityNumber,
					Name = community.CommunityName,
					PhoneNumber = await GetCommunityPhoneNumberAsync(community.CommunityId),
					PostalAddress = await GetCommunityPostalAddressAsync(community.CommunityId),
					StartingAtPrice = await GetCommunityStartingAtPriceAsync(community.CommunityId),
					Pricing = await GetCommunityPricingAsync(community.CommunityId, roomGrouping),
					DigitalAssets = await GetDigitalAssetsAsync(community.CommunityId, languageCulture, community.LanguageCultureCode),
					Attributes = await GetCommunityAttributesAsync(community.CommunityId, languageCulture, community.LanguageCultureCode)
				}
			});
	}

	private async Task<string?> GetCommunityPhoneNumberAsync(int communityId)
	{
		return (await _portfolioContext.CommunityPhoneNumbers
			.FirstOrDefaultAsync(x => x.CommunityId == communityId && x.IsListingNumber))?.PhoneNumber;
	}

	private async Task<PostalAddressResponse?> GetCommunityPostalAddressAsync(int communityId)
	{

		CommunityPostalAddress? communityPostalAddress = await _portfolioContext.CommunityPostalAddresses
			.FirstOrDefaultAsync(x => x.CommunityId == communityId && x.IsListingAddress);
		if (communityPostalAddress is not null)
			return new PostalAddressResponse()
			{
				StreetAddress1 = communityPostalAddress.StreetAddress1,
				StreetAddress2 = communityPostalAddress.StreetAddress2,
				City = communityPostalAddress.City,
				CountryDivision = communityPostalAddress.CountryDivisionCode,
				Country = communityPostalAddress.CountryCode,
				PostalCode = communityPostalAddress.PostalCode
			};
		else
			return default;
	}

}