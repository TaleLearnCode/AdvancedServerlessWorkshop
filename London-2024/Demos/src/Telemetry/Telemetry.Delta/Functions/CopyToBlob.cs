using Azure.Storage.Blobs;
using System.Text;
using System.Text.Json;

namespace TaleLearnCode.FlightTrackingDemo.Telemetry.Delta.Functions;

public class CopyToBlob(ILoggerFactory loggerFactory, BlobContainerClient blobContainerClient)
{
	private readonly ILogger _logger = loggerFactory.CreateLogger<CopyToBlob>();
	private readonly BlobContainerClient _blobContainerClient = blobContainerClient;

	[Function("CopyToBlob")]
	public async Task RunAsync([CosmosDBTrigger(
		databaseName: "%CosmosTelemetryDatabaseName%",
		containerName: "%CosmosTelemetryContainerName%",
		Connection = "CosmosConnectionString",
		LeaseContainerName = "%BlobLeaseContainerName%",
		CreateLeaseContainerIfNotExists = true)] IReadOnlyList<CosmosData.FlightTelemetry> telemetryRecords)
	{
		if (telemetryRecords is not null && telemetryRecords.Count > 0)
		{
			_logger.LogInformation("Documents modified: " + telemetryRecords.Count);
			foreach (CosmosData.FlightTelemetry telemetryRecord in telemetryRecords)
			{
				BlobClient blobClient = _blobContainerClient.GetBlobClient(telemetryRecord.FileName());
				using MemoryStream memoryStream = new(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(telemetryRecord)));
				await blobClient.UploadAsync(memoryStream, true);
				_logger.LogInformation("Uploaded file {FileName} to blob storage", telemetryRecord.FileName());
			}
		}
	}

}