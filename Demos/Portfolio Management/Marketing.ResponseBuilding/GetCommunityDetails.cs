﻿namespace SLS.Marketing.ResponseBuilding;

public class GetCommunityDetails : ResponseBuildingBase, IGetCommunityDetails
{

	public GetCommunityDetails(
		PortfolioContext portfolioContext,
		Container cosmosContainer) : base(portfolioContext, cosmosContainer) { }

	public async Task BuildAsync(string communityNumber)
	{
		Community? community = await _portfolioContext.Communities
			.FirstOrDefaultAsync(x => x.CommunityNumber == communityNumber);
		if (community is not null)
			foreach (string languageCulture in GetCommunityLanguageCultures())
				foreach (RoomGrouping roomGrouping in GetRoomGroupings())
					await Build(community, roomGrouping, languageCulture);
	}

	private async Task<BuildCacheResponseDetail> Build(Community community, RoomGrouping roomGrouping, string languageCulture)
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
					Pricing = await GetCommunityPricingAsync(community.CommunityId, roomGrouping)
				}
			});
	}

	private async Task<string?> GetCommunityPhoneNumberAsync(int communityId)
	{
		return (await _portfolioContext.CommunityPhoneNumbers
			.FirstOrDefaultAsync(x => x.CommunityId == communityId && x.IsListingNumber))?.PhoneNumber;
	}

	private async Task<IPostalAddressResponse?> GetCommunityPostalAddressAsync(int communityId)
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

	private async Task<int> GetCommunityStartingAtPriceAsync(int communityId)
	{

		int availableStartingAtPrice = 0;
		int unavailableStartingAtPrice = 0;

		List<Room>? rooms = await _portfolioContext.Rooms
			.Include(x => x.RoomRates)
				.ThenInclude(x => x.PayorType)
			.Include(x => x.RoomAvailability)
			.Where(x => x.CommunityId == communityId)
			.ToListAsync();
		foreach (Room room in rooms)
		{
			int? displayedRate = GetDisplayedRoomRate(room.RoomRates.FirstOrDefault(x => x.PayorType.IsDefault));
			if (displayedRate is not null && displayedRate > 0)
				if (room.RoomAvailability.ShowAsAvailable)
					availableStartingAtPrice = (availableStartingAtPrice == 0 || displayedRate < availableStartingAtPrice) ? (int)displayedRate : availableStartingAtPrice;
				else
					unavailableStartingAtPrice = (unavailableStartingAtPrice == 0 || displayedRate < unavailableStartingAtPrice) ? (int)displayedRate : unavailableStartingAtPrice;
		}

		return (availableStartingAtPrice > 0) ? availableStartingAtPrice : unavailableStartingAtPrice;

	}

	private async Task<IDictionary<string, IPricingByCareTypeResponse>?> GetCommunityPricingAsync(int communityId, RoomGrouping roomGrouping)
	{

		List<CommunityCareType> communityCareTypes = await _portfolioContext.CommunityCareTypes
			.Include(x => x.CareType)
			.Where(x => x.CommunityId == communityId)
			.ToListAsync();
		if (communityCareTypes.Any())
		{
			Dictionary<string, IPricingByCareTypeResponse> response = new();
			foreach (CareType careType in communityCareTypes.Select(x => x.CareType))
			{
				string careTypeKey = careType.ExternalId ?? careType.CareTypeId.ToString();
				response.TryAdd(careTypeKey, new PricingByCareTypeResponse()
				{
					CareTypeCode = careType.CareTypeCode,
					CareTypeName = careType.CareTypeName,
					RoomTypes = new Dictionary<string, IPricingByRoomTypeResponse>()
				});
				Dictionary<string, CareTypePricingResponses> careTypePricingResponses = new();
				List<CommunityRoomType> communityRoomTypes = await _portfolioContext.CommunityRoomTypes
					.Include(x => x.FloorPlan)
					.Where(x => x.CommunityId == communityId && x.CareTypeId == careType.CareTypeId)
					.ToListAsync();
				foreach (CommunityRoomType communityRoomType in communityRoomTypes)
				{

					(string Key, string Name) roomTypeKeyAndName = await GetRoomTypeKeyAndNameAsync(roomGrouping, communityRoomType.RoomTypeId);
					IPricingByRoomTypeResponse availableResponse = InitializePricingByRoomTypeResponse(roomTypeKeyAndName.Name, communityRoomType.FloorPlan?.DigitalAssetUrl.ToUri());
					IPricingByRoomTypeResponse unavailableResponse = InitializePricingByRoomTypeResponse(roomTypeKeyAndName.Name, communityRoomType.FloorPlan?.DigitalAssetUrl.ToUri());

					List<Room> rooms = await _portfolioContext.Rooms
						.Include(x => x.RoomAvailability)
						.Include(x => x.RoomRates)
							.ThenInclude(x => x.PayorType)
						.Where(x => x.CommunityId == communityId && x.AssignedCareTypeId == careType.CareTypeId && x.RoomTypeId == communityRoomType.RoomTypeId)
						.ToListAsync();
					foreach (Room room in rooms)
					{

						if (room.RoomArea is not null)
						{
							unavailableResponse.AreaRangeStart = (unavailableResponse.AreaRangeStart == 0 || room.RoomArea > unavailableResponse.AreaRangeStart) ? (int)room.RoomArea : unavailableResponse.AreaRangeStart;
							unavailableResponse.AreaRangeEnd = (room.RoomArea > unavailableResponse.AreaRangeEnd) ? (int)room.RoomArea : unavailableResponse.AreaRangeEnd;
							availableResponse.AreaRangeStart = (availableResponse.AreaRangeStart == 0 || room.RoomArea > availableResponse.AreaRangeStart) ? (int)room.RoomArea : availableResponse.AreaRangeStart;
							availableResponse.AreaRangeEnd = (room.RoomArea > availableResponse.AreaRangeEnd) ? (int)room.RoomArea : availableResponse.AreaRangeEnd;
						}

						int? displayedRate = GetDisplayedRoomRate(room.RoomRates.FirstOrDefault(x => x.PayorType.IsDefault));
						if (displayedRate is not null)
						{
							unavailableResponse.StartingAt = (unavailableResponse.StartingAt == 0 || displayedRate < unavailableResponse.StartingAt) ? (int)displayedRate : unavailableResponse.StartingAt;
							unavailableResponse.EndingAt = (displayedRate > unavailableResponse.EndingAt) ? (int)displayedRate : unavailableResponse.EndingAt;
							availableResponse.StartingAt = (availableResponse.StartingAt == 0 || displayedRate < availableResponse.StartingAt) ? (int)displayedRate : availableResponse.StartingAt;
							availableResponse.EndingAt = (displayedRate > availableResponse.EndingAt) ? (int)displayedRate : availableResponse.EndingAt;
						}

						foreach (RoomRate roomRate in room.RoomRates)
						{

							displayedRate = GetDisplayedRoomRate(roomRate) ?? 0;
							string payorTypeKey = roomRate.PayorType.ExternalId ?? roomRate.PayorType.PayorTypeId.ToString();

							unavailableResponse.PricingByPayorType.TryAdd(payorTypeKey, new PricingByPayorTypeResponse()
							{
								PayorType = roomRate.PayorType.PayorTypeName,
								StartingAt = 0,
								EndingAt = 0
							});
							unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt = (unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt == 0 || displayedRate < unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt) ? (int)displayedRate : unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt;
							unavailableResponse.PricingByPayorType[payorTypeKey].EndingAt = (displayedRate > unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt) ? (int)displayedRate : unavailableResponse.PricingByPayorType[payorTypeKey].EndingAt;

							availableResponse.PricingByPayorType.TryAdd(payorTypeKey, new PricingByPayorTypeResponse()
							{
								PayorType = roomRate.PayorType.PayorTypeName,
								StartingAt = 0,
								EndingAt = 0
							});
							availableResponse.PricingByPayorType[payorTypeKey].StartingAt = (availableResponse.PricingByPayorType[payorTypeKey].StartingAt == 0 || displayedRate < availableResponse.PricingByPayorType[payorTypeKey].StartingAt) ? (int)displayedRate : availableResponse.PricingByPayorType[payorTypeKey].StartingAt;
							availableResponse.PricingByPayorType[payorTypeKey].EndingAt = (displayedRate > availableResponse.PricingByPayorType[payorTypeKey].StartingAt) ? (int)displayedRate : availableResponse.PricingByPayorType[payorTypeKey].EndingAt;

						}

						if (room.RoomAvailability.ShowAsAvailable) availableResponse.VacantCount++;
					}

					careTypePricingResponses.TryAdd(roomTypeKeyAndName.Key, new());
					careTypePricingResponses[roomTypeKeyAndName.Key].AddResponses(availableResponse, unavailableResponse);

				}

				foreach (KeyValuePair<string, CareTypePricingResponses> careTypePricingResponse in careTypePricingResponses)
					response[careTypeKey].RoomTypes.TryAdd(careTypePricingResponse.Key, careTypePricingResponse.Value.MergeResponses());

			}
			return response;
		}
		else
		{
			return default;
		}
	}

	private int? GetDisplayedRoomRate(RoomRate? roomRate)
	{
		if (roomRate is not null)
		{
			return (int)(roomRate.DiscountedRate ?? roomRate.BaseRate);
		}
		else
			return default;
	}

	private async Task<(string Key, string Name)> GetRoomTypeKeyAndNameAsync(
		RoomGrouping roomGrouping,
		int roomTypeId)
	{
		RoomType? roomType = await _portfolioContext.RoomTypes
			.Include(x => x.RoomStyle)
			.Include(x => x.RoomTypeCategory)
			.FirstOrDefaultAsync(x => x.RoomTypeId == roomTypeId);
		if (roomType is not null)
		{
			switch (roomGrouping)
			{
				case RoomGrouping.RoomTypeCategory:
					return (roomType.RoomTypeCategory?.ExternalId ?? roomType.RoomTypeCategory?.RoomTypeCategoryId.ToString() ?? roomTypeId.ToString(),
						roomType.RoomTypeCategory?.RoomTypeCategoryName ?? roomTypeId.ToString());
				case RoomGrouping.RoomStyle:
					return (roomType.RoomStyle.ExternalId ?? roomType.RoomStyle.RoomStyleId.ToString(),
						roomType.RoomStyle.RoomStyleName);
				default:
					return (roomType.ExternalId ?? roomType.RoomTypeId.ToString(), roomType.RoomTypeName);
			}
		}
		else
			return (roomTypeId.ToString(), roomTypeId.ToString());
	}

	private IPricingByRoomTypeResponse InitializePricingByRoomTypeResponse(string roomTypeName, Uri? floorPlanUrl)
	{
		return new PricingByRoomTypeResponse()
		{
			RoomType = roomTypeName,
			VacantCount = 0,
			FloorPlanUrl = floorPlanUrl,
			ShowPricing = true,
			StartingAt = 0,
			EndingAt = 0,
			PricingByPayorType = new Dictionary<string, IPricingByPayorTypeResponse>()
		};
	}

	private List<string> GetCommunityLanguageCultures()
	{
		return new() { "en-US", string.Empty };
	}

	private List<RoomGrouping> GetRoomGroupings()
	{
		return new() { RoomGrouping.RoomType, RoomGrouping.RoomTypeCategory, RoomGrouping.RoomStyle };
	}

}