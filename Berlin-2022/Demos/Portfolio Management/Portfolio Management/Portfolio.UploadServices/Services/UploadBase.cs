using TinyCsvParser;

namespace SLS.Portfolio.UploadServices;

internal abstract class UploadBase
{
	protected readonly CsvReaderOptions _csvReaderOptions = new(new[] { Environment.NewLine });
}