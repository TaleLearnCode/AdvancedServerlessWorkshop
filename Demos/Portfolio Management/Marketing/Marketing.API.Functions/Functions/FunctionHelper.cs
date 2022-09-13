namespace SLS.Marketing;

internal static class FunctionHelper
{

	internal static RoomGrouping GetRoomGrouping(HttpRequestData request)
	{
		RoomGrouping roomGrouping = RoomGrouping.RoomStyle;
		string? roomGroupingInput = request.GetQueryStringValue("roomGrouping");
		if (roomGroupingInput is not null)
			roomGrouping = roomGroupingInput.ToUpperInvariant() switch
			{
				"ROOMSTYLE" => RoomGrouping.RoomStyle,
				"ROOMTYPE" => RoomGrouping.RoomType,
				"ROOMTYPECATEGORY" => RoomGrouping.RoomTypeCategory,
				_ => RoomGrouping.RoomStyle
			};
		return roomGrouping;
	}

}