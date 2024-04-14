using Azure.Core.Serialization;
using Microsoft.Azure.Cosmos;
using System.Text.Json;

#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.Common.CosmosData;

/// <summary>
/// Uses <see cref="JsonObjectSerializer"/> which leverages System.Text.Json providing a simple API to interact with on the Azure SDKs.
/// </summary>
public class CosmosSystemTextJsonSerializer(JsonSerializerOptions jsonSerializerOptions) : CosmosSerializer
{

	private readonly JsonObjectSerializer _systemTextJsonSerializer = new(jsonSerializerOptions);

	public override T FromStream<T>(Stream stream)
	{
		using (stream)
		{
			if (stream.CanSeek && stream.Length == 0)
				return default;

			if (typeof(Stream).IsAssignableFrom(typeof(T)))
				return (T)(object)stream;

			return (T)_systemTextJsonSerializer.Deserialize(stream, typeof(T), default);
		}
	}

	public override Stream ToStream<T>(T input)
	{
		MemoryStream streamPayload = new();
		_systemTextJsonSerializer.Serialize(streamPayload, input, input.GetType(), default);
		streamPayload.Position = 0;
		return streamPayload;
	}

}