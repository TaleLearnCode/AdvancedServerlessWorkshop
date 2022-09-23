using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SLS.Portfolio.UploadServices
{
	public class UploadMonitor
	{
		private readonly ILogger _logger;

		public UploadMonitor(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<UploadMonitor>();
		}

		[Function("UploadMonitor")]
		public async Task RunAsync([BlobTrigger("uploads/{fileName}", Connection = "StorageConnectionString")] string blobContents, string fileName)
		{
			_logger.LogInformation($"Processing upload: {fileName}");
			await UploadService.ProcessUploadAsync(fileName, blobContents);
		}
	}
}
