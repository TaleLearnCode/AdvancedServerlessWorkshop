namespace SLS.Portfolio.UploadServices;

public sealed class UploadService
{

	public static async Task ProcessUploadAsync(string fileName, string blobContents)
	{
		UploadDirective uploadDirective = new(fileName);
		switch (uploadDirective.UploadType)
		{
			case UploadType.RoomRate:
				await UploadRoomRates.ProcessUploadAsync(uploadDirective, blobContents);
				break;
		}
	}

}