namespace SLS.Portfolio.UploadServices;

internal class UploadDirective
{

	private const string _RoomRate = "ROOMRATE";

	internal UploadDirective(string fileName)
	{

		string[]? splitFileName = fileName.Split('_');

		if (splitFileName.Length > 0)
			UploadType = splitFileName[0].ToUpperInvariant() switch
			{
				_RoomRate => UploadType.RoomRate,
				_ => UploadType.InvalidType,
			};

		if (splitFileName.Length > 1)
			Delimiter = splitFileName[1];

		if (splitFileName.Length > 2)
			Options = splitFileName[2];

	}
	internal UploadType UploadType { get; set; }
	internal string? Delimiter { get; set; }
	internal string? Options { get; set; }
}
