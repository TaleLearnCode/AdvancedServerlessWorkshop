﻿namespace SLS.Marketing.ResponseBuilding;

public abstract class CacheResponseBase
{

	private readonly Container _cosmosContainer;

	protected CacheResponseBase(Container container)
	{
		_cosmosContainer = container;
	}

	protected async Task<CacheResponseResult> UpsertCosmosItemAsync<T>(
		string partitionKey,
		T item) where T : BaseCachedResponse
	{
		try
		{
			if (await CosmosItemExists<T>(item.Id, partitionKey))
				await _cosmosContainer.ReplaceItemAsync(item, item.Id, new PartitionKey(partitionKey));
			else
				await _cosmosContainer.CreateItemAsync(item, new PartitionKey(partitionKey));
		}
		catch (Exception ex)
		{
			return new()
			{
				CosmosId = item.Id,
				Status = "Failed",
				ExceptionDetail = ex.Message
			};
		}
		return new()
		{
			CosmosId = item.Id,
			Status = "Success"
		};
	}

	protected async Task<bool> CosmosItemExists<T>(
		string id,
		string partitionKey)
	{
		try
		{
			await _cosmosContainer.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
			return true;
		}
		catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			return false;
		}
	}

	protected static async Task<Community?> GetCommunityAsync(PortfolioContext portfolioContext, string communityNumber)
	{
		return await portfolioContext.Communities
			.FirstOrDefaultAsync(x => x.CommunityNumber == communityNumber);
	}

	protected static List<string> GetCommunityLanguageCultures()
	{
		// TODO: Add logic to get the list for the community
		return new() { "en-US", string.Empty };
	}

	protected static async Task<Dictionary<string, List<DigitalAssetResponse>>?> GetDigitalAssetsAsync(
		PortfolioContext portfolioContext,
		int communityId,
		string languageCulture,
		string? defaultLanguageCulture)
	{
		List<CommunityDigitalAsset>? communityDigitalAssets = await portfolioContext.CommunityDigitalAssets
			.Include(x => x.DigitalAsset)
				.ThenInclude(x => x.DigitalAssetType)
			.Where(x => x.CommunityId == communityId)
			.ToListAsync();
		if (communityDigitalAssets is not null)
		{
			Dictionary<string, List<DigitalAssetResponse>> response = new();
			foreach (CommunityDigitalAsset communityDigitalAsset in communityDigitalAssets)
			{
				response.TryAdd(communityDigitalAsset.DigitalAsset.DigitalAssetType.Discriminator, new List<DigitalAssetResponse>());
				response[communityDigitalAsset.DigitalAsset.DigitalAssetType.Discriminator].Add(new DigitalAssetResponse()
				{
					Discriminator = communityDigitalAsset.DigitalAsset.Discriminator,
					Name = communityDigitalAsset.DigitalAsset.DigitalAssetName,
					Caption = await GetContentCopyAsync(portfolioContext, communityDigitalAsset.DigitalAsset.CaptionId, languageCulture, defaultLanguageCulture),
					AltText = await GetContentCopyAsync(portfolioContext, communityDigitalAsset.DigitalAsset.AltTextId, languageCulture, defaultLanguageCulture),
					Url = communityDigitalAsset.DigitalAsset.DigitalAssetUrl.ToUri(),
					ThumbnailUrl = communityDigitalAsset.DigitalAsset.ThumbnailUrl.ToUri(),
					IsFeatured = communityDigitalAsset.IsFeatured
				});
			}
			return response;
		}
		else
			return default;
	}

	protected static async Task<string?> GetContentCopyAsync(
		PortfolioContext portfolioContext,
		int? contentId,
		string languageCulture,
		string? defaultLanguageCulture)
	{
		if (contentId is not null)
		{
			Content? content = await portfolioContext.Contents
				.Include(x => x.ContentCopies)
				.FirstOrDefaultAsync(x => x.ContentId == contentId);
			if (content is not null && content.ContentCopies.Any())
			{
				return content.ContentCopies.FirstOrDefault(x => x.LanguageCultureCode == languageCulture)?.CopyText ??
					content.ContentCopies.FirstOrDefault(x => x.LanguageCultureCode == defaultLanguageCulture)?.CopyText ??
					content.ContentCopies.First().CopyText;
			}
			else
				return default;
		}
		return default;
	}

	protected static async Task<int> GetCommunityStartingAtPriceAsync(PortfolioContext portfolioContext, int communityId)
	{

		int availableStartingAtPrice = 0;
		int unavailableStartingAtPrice = 0;

		List<Room>? rooms = await portfolioContext.Rooms
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
					availableStartingAtPrice = availableStartingAtPrice == 0 || displayedRate < availableStartingAtPrice ? (int)displayedRate : availableStartingAtPrice;
				else
					unavailableStartingAtPrice = unavailableStartingAtPrice == 0 || displayedRate < unavailableStartingAtPrice ? (int)displayedRate : unavailableStartingAtPrice;
		}

		return availableStartingAtPrice > 0 ? availableStartingAtPrice : unavailableStartingAtPrice;

	}

	protected static int? GetDisplayedRoomRate(RoomRate? roomRate)
	{
		if (roomRate is not null)
		{
			return (int)(roomRate.DiscountedRate ?? roomRate.BaseRate);
		}
		else
			return default;
	}

	protected static async Task<Dictionary<string, PricingByCareTypeResponse>?> GetCommunityPricingAsync(
		PortfolioContext portfolioContext,
		int communityId,
		RoomGrouping roomGrouping)
	{

		List<CommunityCareType> communityCareTypes = await portfolioContext.CommunityCareTypes
			.Include(x => x.CareType)
			.Where(x => x.CommunityId == communityId)
			.ToListAsync();
		if (communityCareTypes.Any())
		{
			Dictionary<string, PricingByCareTypeResponse> response = new();
			foreach (CareType careType in communityCareTypes.Select(x => x.CareType))
			{
				string careTypeKey = careType.ExternalId ?? careType.CareTypeId.ToString();
				response.TryAdd(careTypeKey, new PricingByCareTypeResponse()
				{
					CareTypeCode = careType.CareTypeCode,
					CareTypeName = careType.CareTypeName,
					RoomTypes = new Dictionary<string, PricingByRoomTypeResponse>()
				});
				Dictionary<string, CareTypePricingResponses> careTypePricingResponses = new();
				List<CommunityRoomType> communityRoomTypes = await portfolioContext.CommunityRoomTypes
					.Include(x => x.FloorPlan)
					.Where(x => x.CommunityId == communityId && x.CareTypeId == careType.CareTypeId)
					.ToListAsync();
				foreach (CommunityRoomType communityRoomType in communityRoomTypes)
				{

					(string Key, string Name) = await GetRoomTypeKeyAndNameAsync(portfolioContext, roomGrouping, communityRoomType.RoomTypeId);
					PricingByRoomTypeResponse availableResponse = InitializePricingByRoomTypeResponse(Name, communityRoomType.FloorPlan?.DigitalAssetUrl.ToUri());
					PricingByRoomTypeResponse unavailableResponse = InitializePricingByRoomTypeResponse(Name, communityRoomType.FloorPlan?.DigitalAssetUrl.ToUri());

					List<Room> rooms = await portfolioContext.Rooms
						.Include(x => x.RoomAvailability)
						.Include(x => x.RoomRates)
							.ThenInclude(x => x.PayorType)
						.Where(x => x.CommunityId == communityId && x.AssignedCareTypeId == careType.CareTypeId && x.RoomTypeId == communityRoomType.RoomTypeId)
						.ToListAsync();
					foreach (Room room in rooms)
					{

						if (room.RoomArea is not null)
						{
							unavailableResponse.AreaRangeStart = unavailableResponse.AreaRangeStart == 0 || room.RoomArea > unavailableResponse.AreaRangeStart ? (int)room.RoomArea : unavailableResponse.AreaRangeStart;
							unavailableResponse.AreaRangeEnd = room.RoomArea > unavailableResponse.AreaRangeEnd ? (int)room.RoomArea : unavailableResponse.AreaRangeEnd;
							availableResponse.AreaRangeStart = availableResponse.AreaRangeStart == 0 || room.RoomArea > availableResponse.AreaRangeStart ? (int)room.RoomArea : availableResponse.AreaRangeStart;
							availableResponse.AreaRangeEnd = room.RoomArea > availableResponse.AreaRangeEnd ? (int)room.RoomArea : availableResponse.AreaRangeEnd;
						}

						int? displayedRate = GetDisplayedRoomRate(room.RoomRates.FirstOrDefault(x => x.PayorType.IsDefault));
						if (displayedRate is not null)
						{
							unavailableResponse.StartingAt = unavailableResponse.StartingAt == 0 || displayedRate < unavailableResponse.StartingAt ? (int)displayedRate : unavailableResponse.StartingAt;
							unavailableResponse.EndingAt = displayedRate > unavailableResponse.EndingAt ? (int)displayedRate : unavailableResponse.EndingAt;
							availableResponse.StartingAt = availableResponse.StartingAt == 0 || displayedRate < availableResponse.StartingAt ? (int)displayedRate : availableResponse.StartingAt;
							availableResponse.EndingAt = displayedRate > availableResponse.EndingAt ? (int)displayedRate : availableResponse.EndingAt;
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
							unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt = unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt == 0 || displayedRate < unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt ? (int)displayedRate : unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt;
							unavailableResponse.PricingByPayorType[payorTypeKey].EndingAt = displayedRate > unavailableResponse.PricingByPayorType[payorTypeKey].StartingAt ? (int)displayedRate : unavailableResponse.PricingByPayorType[payorTypeKey].EndingAt;

							availableResponse.PricingByPayorType.TryAdd(payorTypeKey, new PricingByPayorTypeResponse()
							{
								PayorType = roomRate.PayorType.PayorTypeName,
								StartingAt = 0,
								EndingAt = 0
							});
							availableResponse.PricingByPayorType[payorTypeKey].StartingAt = availableResponse.PricingByPayorType[payorTypeKey].StartingAt == 0 || displayedRate < availableResponse.PricingByPayorType[payorTypeKey].StartingAt ? (int)displayedRate : availableResponse.PricingByPayorType[payorTypeKey].StartingAt;
							availableResponse.PricingByPayorType[payorTypeKey].EndingAt = displayedRate > availableResponse.PricingByPayorType[payorTypeKey].StartingAt ? (int)displayedRate : availableResponse.PricingByPayorType[payorTypeKey].EndingAt;

						}

						if (room.RoomAvailability.ShowAsAvailable) availableResponse.VacantCount++;
					}

					careTypePricingResponses.TryAdd(Key, new());
					careTypePricingResponses[Key].AddResponses(availableResponse, unavailableResponse);

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

	protected static async Task<(string Key, string Name)> GetRoomTypeKeyAndNameAsync(
		PortfolioContext portfolioContext,
		RoomGrouping roomGrouping,
		int roomTypeId)
	{
		RoomType? roomType = await portfolioContext.RoomTypes
			.Include(x => x.RoomStyle)
			.Include(x => x.RoomTypeCategory)
			.FirstOrDefaultAsync(x => x.RoomTypeId == roomTypeId);
		if (roomType is not null)
		{
			return roomGrouping switch
			{
				RoomGrouping.RoomTypeCategory => (roomType.RoomTypeCategory?.ExternalId ?? roomType.RoomTypeCategory?.RoomTypeCategoryId.ToString() ?? roomTypeId.ToString(),
										roomType.RoomTypeCategory?.RoomTypeCategoryName ?? roomTypeId.ToString()),
				RoomGrouping.RoomStyle => (roomType.RoomStyle.ExternalId ?? roomType.RoomStyle.RoomStyleId.ToString(),
										roomType.RoomStyle.RoomStyleName),
				_ => (roomType.ExternalId ?? roomType.RoomTypeId.ToString(), roomType.RoomTypeName),
			};
		}
		else
			return (roomTypeId.ToString(), roomTypeId.ToString());
	}
	protected static PricingByRoomTypeResponse InitializePricingByRoomTypeResponse(string roomTypeName, Uri? floorPlanUrl)
	{
		return new PricingByRoomTypeResponse()
		{
			RoomType = roomTypeName,
			VacantCount = 0,
			FloorPlanUrl = floorPlanUrl,
			ShowPricing = true,
			StartingAt = 0,
			EndingAt = 0,
			PricingByPayorType = new Dictionary<string, PricingByPayorTypeResponse>()
		};
	}

	protected static List<RoomGrouping> GetRoomGroupings()
	{
		return new() { RoomGrouping.RoomType, RoomGrouping.RoomTypeCategory, RoomGrouping.RoomStyle };
	}

	protected static async Task<Dictionary<string, List<CommunityAttributeResponse>>?> GetCommunityAttributesAsync(
		PortfolioContext portfolioContext,
		int communityId,
		string languageCulture,
		string? defaultLanguageCulture)
	{
		List<CommunityCommunityAttribute>? communityCommunityAttributes = await portfolioContext.CommunityCommunityAttributes
			.Include(x => x.CommunityAttribute)
				.ThenInclude(x => x.CommunityAttributeType)
			.Include(x => x.CommunityAttribute)
				.ThenInclude(x => x.Icon)
			.Include(x => x.CommunityAttribute)
				.ThenInclude(x => x.Label)
			.Where(x => x.CommunityId == communityId)
			.ToListAsync();
		if (communityCommunityAttributes.Any())
		{
			Dictionary<string, List<CommunityAttributeResponse>> response = new();
			foreach (CommunityAttribute? communityAttribute in communityCommunityAttributes.Select(x => x.CommunityAttribute))
			{
				string attributeTypeKey = communityAttribute.CommunityAttributeType.ExternalId ?? communityAttribute.CommunityAttributeTypeId.ToString();
				response.TryAdd(attributeTypeKey, new List<CommunityAttributeResponse>());
				string? label = await GetContentCopyAsync(portfolioContext, communityAttribute.LabelId, languageCulture, defaultLanguageCulture);
				if (label is not null)
				{
					response[attributeTypeKey].Add(new()
					{
						Label = label,
						IconUrl = communityAttribute.Icon.DigitalAssetUrl.ToUri(),
						AltText = await GetContentCopyAsync(portfolioContext, communityAttribute.Icon.AltTextId, languageCulture, defaultLanguageCulture)
					});
				}
			}
			return response;
		}
		else
			return default;
	}

	protected static async Task<string?> GetCommunityPhoneNumberAsync(PortfolioContext portfolioContext, int communityId)
	{
		return (await portfolioContext.CommunityPhoneNumbers
			.FirstOrDefaultAsync(x => x.CommunityId == communityId && x.IsListingNumber))?.PhoneNumber;
	}

	protected static async Task<PostalAddressResponse?> GetCommunityPostalAddressAsync(PortfolioContext portfolioContext, int communityId)
	{

		CommunityPostalAddress? communityPostalAddress = await portfolioContext.CommunityPostalAddresses
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