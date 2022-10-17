
using Drone.Domain.Entities;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

int _deviceCount = GetIntegerValue("Number of devices to simulate: ");
int _sleepTime = GetIntegerValue("Milliseconds in-between device updates: ");

Random _random = new Random(100);
HttpClient _httpClient = new HttpClient();
JsonSerializerOptions _jsonSerializerOptions = new()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
};


do
{
	for (int i = 1; i <= _deviceCount; i++)
	{
		DeviceState deviceState = GenerateTelemetry(i);
		HttpStatusCode updateStatus = await SendTelemetryAsync(deviceState);
		if (updateStatus == HttpStatusCode.OK)
			Console.ForegroundColor = ConsoleColor.Green;
		else
			Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"{updateStatus} : {deviceState.DeviceId}");
	}
	Thread.Sleep(_sleepTime);
} while (true);

int GetIntegerValue(string prompt)
{
	do
	{
		Console.Write(prompt);
		string? something = Console.ReadLine();
		if (int.TryParse(something, out int result))
			return result;
		Console.Beep();
	} while (true);
}

DeviceState GenerateTelemetry(int deviceId)
{
	Random random = new Random(100);
	return new()
	{
		Id = $"Device_{deviceId}",
		DeviceId = $"Device_{deviceId}",
		AccelerometerOK = GetRandomBool(),
		Altitude = _random.Next(0, 15000),
		Battery = _random.Next(1, 101),
		FlightMode = _random.Next(1, 5),
		GyrometerOK = GetRandomBool(),
		Latitude = GetRandomDouble(-89.99999, 89.99999, 5),
		Longitude = GetRandomDouble(-179.99999, 179.99999, 5),
		MagnetometerOK = GetRandomBool()
	};
}

bool GetRandomBool()
{
	bool[] arr = { true, false };
	return arr[_random.Next(2)];
}

double GetRandomDouble(double minValue, double maxValue, int decimalPlaces)
{
	double randNumber = _random.NextDouble() * (maxValue - minValue) + minValue;
	return Convert.ToDouble(randNumber.ToString("f" + decimalPlaces));
}

async Task<HttpStatusCode> SendTelemetryAsync(DeviceState deviceState)
{
	string json = JsonSerializer.Serialize(deviceState, _jsonSerializerOptions);
	using StringContent jsonContent = new(json, Encoding.UTF8, "application/json");
	using HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:7172/api/UpdateDeviceState", jsonContent);
	return response.StatusCode;
}