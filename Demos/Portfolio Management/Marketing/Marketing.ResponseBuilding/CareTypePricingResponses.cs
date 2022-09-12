namespace SLS.Marketing.ResponseBuilding;

internal class CareTypePricingResponses
{

	internal List<IPricingByRoomTypeResponse> AvailableResponses { get; set; } = new List<IPricingByRoomTypeResponse>();

	internal List<IPricingByRoomTypeResponse> UnavailableResponses { get; set; } = new List<IPricingByRoomTypeResponse>();

	internal void AddResponses(IPricingByRoomTypeResponse availableResponse, IPricingByRoomTypeResponse unavailalbeResponse)
	{
		AvailableResponses.Add(availableResponse);
		UnavailableResponses.Add(unavailalbeResponse);
	}

	internal IPricingByRoomTypeResponse MergeResponses()
	{
		IPricingByRoomTypeResponse response = InitializePricingByRoomTypeResponse(AvailableResponses[0].RoomType, AvailableResponses[0].FloorPlanUrl);
		foreach (IPricingByRoomTypeResponse roomTypeResponse in (AvailableResponses.FirstOrDefault(x => x.VacantCount > 0) is not null) ? AvailableResponses : UnavailableResponses)
		{
			response.AreaRangeStart = (response.AreaRangeStart == 0 || roomTypeResponse.AreaRangeStart < response.AreaRangeStart) ? roomTypeResponse.AreaRangeStart : response.AreaRangeStart;
			response.AreaRangeEnd = (roomTypeResponse.AreaRangeEnd > response.AreaRangeEnd) ? roomTypeResponse.AreaRangeEnd : response.AreaRangeEnd;
			response.VacantCount += roomTypeResponse.VacantCount;
			response.ShowPricing = (!roomTypeResponse.ShowPricing) ? roomTypeResponse.ShowPricing : response.ShowPricing;
			response.StartingAt = (response.StartingAt == 0 || roomTypeResponse.StartingAt < response.StartingAt) ? roomTypeResponse.StartingAt : response.StartingAt;
			response.EndingAt = (response.EndingAt == 0 || roomTypeResponse.EndingAt > response.EndingAt) ? roomTypeResponse.EndingAt : response.EndingAt;
			foreach (KeyValuePair<string, IPricingByPayorTypeResponse> pricingByPayorType in roomTypeResponse.PricingByPayorType)
			{
				response.PricingByPayorType.TryAdd(pricingByPayorType.Key, new PricingByPayorTypeResponse()
				{
					PayorType = pricingByPayorType.Value.PayorType,
					StartingAt = pricingByPayorType.Value.StartingAt,
					EndingAt = pricingByPayorType.Value.EndingAt
				});
				response.PricingByPayorType[pricingByPayorType.Key].StartingAt =
					(response.PricingByPayorType[pricingByPayorType.Key].StartingAt == 0
					|| pricingByPayorType.Value.StartingAt < response.PricingByPayorType[pricingByPayorType.Key].StartingAt)
					? pricingByPayorType.Value.StartingAt : response.PricingByPayorType[pricingByPayorType.Key].StartingAt;
				response.PricingByPayorType[pricingByPayorType.Key].EndingAt =
					(pricingByPayorType.Value.EndingAt > response.PricingByPayorType[pricingByPayorType.Key].EndingAt)
					? pricingByPayorType.Value.EndingAt : response.PricingByPayorType[pricingByPayorType.Key].EndingAt;
			}
		}
		return response;
	}

	internal static IPricingByRoomTypeResponse InitializePricingByRoomTypeResponse(string roomTypeName, Uri? floorPlanUrl)
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


}