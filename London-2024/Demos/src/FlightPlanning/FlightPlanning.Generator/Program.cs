using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaleLearnCode.FlightTrackingDemo.FlightPlanGenerator.Extensions;
using TaleLearnCode.FlightTrackingDemo.FlightPlanGenerator.Models;
using TaleLearnCode.FlightTrackingDemo.FlightPlanGenerator.StaticData;
using TaleLearnCode.FlightTrackingDemo.Messages;
using TaleLearnCode.FlightTrackingDemo.SqlData.Models;

const string _submitForApprovalUrl = "http://localhost:7255/SubmitFlightPlan";

FlightTrackerContext context = new();
Dictionary<string, Airport> airports = context.Airports.ToDictionary(a => a.Iatacode, a => a);
Dictionary<string, AircraftType> aircraftTypes = context.AircraftTypes.ToDictionary(a => a.AircraftTypeCode, a => a);
Dictionary<string, Airline> airlines = context.Airlines.ToDictionary(a => a.Iatacode, a => a);
Dictionary<string, string> flightStatuses = context.FlightStatuses.ToDictionary(f => f.FlightStatusCode, f => f.FlightStatusName);

using HttpClient httpClient = new();

JsonSerializerOptions _jsonSerializerOptions = new()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
	WriteIndented = true
};


bool continueEnteringFlights = true;
do
{

	PrintBanner();

	string airlineCode = PromptForAirline();
	string flightNumber = PromptForFlightNumber();
	string originAirportCode = PromptForAirport("Enter the departure airport code: ");
	string destinationAirportCode = PromptForAirport("Enter the arrival airport code: ");
	string AircraftTypeCode = PromptForAircraftType();
	int telemetryInterval = PromptForInt("Enter the telemetry interval in seconds: ") * 1000;

	await SubmitFlightPlanForApprovalAsync(airlineCode, flightNumber, originAirportCode, destinationAirportCode, AircraftTypeCode);

	Console.WriteLine();
	continueEnteringFlights = PromptForBool("Do you want to enter another flight? (Y/N) ");

} while (continueEnteringFlights);




void PrintBanner()
{
	Console.Clear();
	Console.ForegroundColor = ConsoleColor.White;
	Console.BackgroundColor = ConsoleColor.Blue;
	Console.WriteLine(@"___________.__  .__       .__     __    __________.__                        ");
	Console.WriteLine(@"\_   _____/|  | |__| ____ |  |___/  |_  \______   \  | _____    ____   ______");
	Console.WriteLine(@" |    __)  |  | |  |/ ___\|  |  \   __\  |     ___/  | \__  \  /    \ /  ___/");
	Console.WriteLine(@" |     \   |  |_|  / /_/  >   Y  \  |    |    |   |  |__/ __ \|   |  \\___ \ ");
	Console.WriteLine(@" \___  /   |____/__\___  /|___|  /__|    |____|   |____(____  /___|  /____  >");
	Console.WriteLine(@"     \/           /_____/      \/                           \/     \/     \/ ");
	Console.ResetColor();
	Console.WriteLine();
}


string PromptForAirline()
{
	string? airline = null;
	do
	{
		Console.WriteLine();
		Console.Write("Enter the airline code: ");
		airline = Console.ReadLine();
		if (airline?.ToUpper() == "LIST")
		{
			PrintAirlines();
			airline = null;
		}
		else if (airline is null || !airlines.ContainsKey(airline))
		{
			airline = null;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Beep();
			Console.WriteLine("Invalid airline specified.");
			Console.ResetColor();
		}
	} while (airline is null);
	return airline;
}

void PrintAirlines()
{
	Console.WriteLine();
	Console.WriteLine("Airlines");
	Console.WriteLine("========");
	foreach (Airline airline in context.Airlines)
		Console.WriteLine($"{airline.Iatacode} - {airline.AirlineName}");
}

string PromptForFlightNumber()
{
	string? flightNumber = null;
	do
	{
		Console.WriteLine();
		Console.Write("Enter the flight number: ");
		flightNumber = Console.ReadLine();
		if (flightNumber is null || flightNumber.Length != 4 || !int.TryParse(flightNumber, out _))
		{
			flightNumber = null;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Beep();
			Console.WriteLine("Invalid flight number specified.");
			Console.ResetColor();
		}
	} while (flightNumber is null);
	return flightNumber;
}

string PromptForAirport(string prompt)
{
	if (!prompt.EndsWith(' ')) prompt += ' ';
	string? airport = null;
	do
	{
		Console.WriteLine();
		Console.Write(prompt);
		airport = Console.ReadLine();
		if (airport?.ToUpper() == "LIST")
		{
			PrintAirports();
			airport = null;
		}
		else if (airport is null || !airports.ContainsKey(airport))
		{
			airport = null;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Beep();
			Console.WriteLine("Invalid airport specified.");
			Console.ResetColor();
		}
	} while (airport is null);
	return airport;
}

void PrintAirports()
{
	Console.WriteLine();
	Console.WriteLine("Airports");
	Console.WriteLine("========");
	foreach (Airport airport in context.Airports)
		Console.WriteLine($"{airport.Iatacode} - {airport.AirportName} ({airport.CityName}, {airport.CountryCode})");
}

string PromptForAircraftType()
{
	string? aircraftType = null;
	do
	{
		Console.WriteLine();
		Console.Write("Enter the aircraft type code: ");
		aircraftType = Console.ReadLine();
		if (aircraftType?.ToUpper() == "LIST")
		{
			PrintAircraftTypes();
			aircraftType = null;
		}
		else if (aircraftType is null || !aircraftTypes.ContainsKey(aircraftType))
		{
			aircraftType = null;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Beep();
			Console.WriteLine("Invalid aircraft type specified.");
			Console.ResetColor();
		}
	} while (aircraftType is null);
	return aircraftType;
}

void PrintAircraftTypes()
{
	Console.WriteLine();
	Console.WriteLine("Aircraft Types");
	Console.WriteLine("==============");
	foreach (AircraftType aircraftType in context.AircraftTypes)
		Console.WriteLine($"{aircraftType.AircraftTypeCode} - {aircraftType.AircraftTypeName}");
}

int PromptForInt(string prompt)
{
	if (!prompt.EndsWith(' ')) prompt += ' ';
	int value;
	do
	{
		Console.WriteLine();
		Console.Write(prompt);
	} while (!int.TryParse(Console.ReadLine(), out value));
	return value;
}

bool PromptForBool(string prompt)
{
	if (!prompt.EndsWith(' ')) prompt += ' ';
	string? response = null;
	do
	{
		Console.WriteLine();
		Console.Write(prompt);
		response = Console.ReadLine();
	} while (response?.ToUpper() != "Y" && response?.ToUpper() != "N");
	return response.Equals("Y", StringComparison.CurrentCultureIgnoreCase);
}

async Task<bool> SubmitFlightPlanForApprovalAsync(
	string airlineCode,
	string flightNumber,
	string originAirportCode,
	string destinationAirportCode,
	string aircraftTypeCode)
{

	try
	{
		FlightPlanMessage flightPlanMessage = BuildFlightPlan(airlineCode, flightNumber, originAirportCode, destinationAirportCode, aircraftTypeCode);
		DisplayFlightPlan(flightPlanMessage);
		StringContent content = new StringContent(JsonSerializer.Serialize(flightPlanMessage, _jsonSerializerOptions), Encoding.UTF8, "application/json");
		HttpResponseMessage response = await httpClient.PostAsync(_submitForApprovalUrl, content);
		if (response.IsSuccessStatusCode)
		{
			string responseContent = await response.Content.ReadAsStringAsync();
			return true;
		}
		else
		{
			Console.WriteLine("Error: " + response.StatusCode);
			return false;
		}
	}
	catch (Exception ex)
	{
		Console.WriteLine("Exception" + ex.Message);
		return false;
	}
}

FlightPlanMessage BuildFlightPlan(
	string airlineCode,
	string flightNumber,
	string originAirportCode,
	string destinationAirportCode,
	string aircraftTypeCode)
{

	Airport originAirport = airports[originAirportCode];
	Geocoordinate originCoordinates = new((double)originAirport.Latitude, (double)originAirport.Longitude);

	Airport destinationAirport = airports[destinationAirportCode];
	Geocoordinate destinationCoordinates = new((double)destinationAirport.Latitude, (double)destinationAirport.Longitude);

	AircraftType aircraftType = aircraftTypes[aircraftTypeCode];

	double distance = CalculateDistance(originCoordinates, destinationCoordinates);

	DateTime departureTime = DateTime.UtcNow.AddMinutes(5);

	return new()
	{
		Id = Guid.NewGuid(),
		Airline = airlineCode,
		FlightNumber = flightNumber,
		OriginAirport = originAirportCode,
		DestinationAirport = destinationAirportCode,
		AircraftType = aircraftTypeCode,
		DepartureTime = departureTime,
		ArrivalTime = CalculateArrivalTime(originCoordinates, destinationCoordinates, departureTime, aircraftType.CruiseSpeedLower),
		Bearing = CalculateBearing(originCoordinates, destinationCoordinates),
		RotationalSpeed = GenerateRandomValue(aircraftType.RotationalSpeedLower, aircraftType.RotationalSpeedUpper),
		InitialClimbAltitude = GenerateRandomValue(aircraftType.InitialClimbAltitudeLower, aircraftType.InitialClimbAltitudeUpper),
		CruiseAltitude = GenerateRandomValue(aircraftType.CruiseAltitudeLower, aircraftType.CruiseAltitudeUpper),
		StartDescentDistance = GenerateRandomValue(WeightsAndMeasurements.DescentRangeLower, WeightsAndMeasurements.DescentRangeUpper),
		StartApproachAltitude = GenerateRandomValue(WeightsAndMeasurements.ApproachAltitudeLower, WeightsAndMeasurements.ApproachAltitudeUpper),
		LandingSpeed = GenerateRandomValue(aircraftType.LandingSpeedLower, aircraftType.LandingSpeedUpper),
		CurrentFlightPhase = FlightPhases.Scheduled
	};

}

static double CalculateDistance(Geocoordinate origin, Geocoordinate destination)
{
	// Method to calculate distance between two points using Haversine formula
	double dLat = (destination.Latitude - origin.Latitude) * (Math.PI / 180);
	double dLon = (destination.Longitude - origin.Longitude) * (Math.PI / 180);
	double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(origin.Latitude * (Math.PI / 180)) * Math.Cos(destination.Latitude * (Math.PI / 180)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
	double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
	double distance = 6371 * c; // Distance in km
	return distance;
}

DateTime CalculateArrivalTime(Geocoordinate origin, Geocoordinate destination, DateTime departureTime, double averageSpeed)
{

	// Determine the distance
	double distance = CalculateDistance(origin, destination);

	// Calculate the estimated time in hours
	double estimatedTimeHours = distance / averageSpeed;

	// Add the estimated time to departure time to get the arrival time and add time for non-cruise
	DateTime estimatedArrivalTime = departureTime.AddHours(estimatedTimeHours).AddMinutes(40);

	return estimatedArrivalTime;

}

static double CalculateBearing(
	Geocoordinate originCoordinates,
	Geocoordinate destinationCoordinates)
{
	double dLon = destinationCoordinates.Longitude - originCoordinates.Longitude;
	double y = Math.Sin(dLon) * Math.Cos(destinationCoordinates.Latitude);
	double x = Math.Cos(originCoordinates.Latitude) * Math.Sin(destinationCoordinates.Latitude) - Math.Sin(originCoordinates.Latitude) * Math.Cos(destinationCoordinates.Latitude) * Math.Cos(dLon);
	double bearing = Math.Atan2(y, x);
	bearing *= (180 / Math.PI);
	bearing = (bearing + 360) % 360;
	return bearing;
}

int GenerateRandomValue(int minValue, int maxValue) => (maxValue <= minValue) ? minValue : new Random().Next(minValue, maxValue + 1);

void DisplayFlightPlan(FlightPlanMessage flightPlanMessage)
{
	Airport originAirport = airports[flightPlanMessage.OriginAirport];
	Airport destinationAirport = airports[flightPlanMessage.DestinationAirport];
	Console.WriteLine();
	Console.WriteLine();
	Console.ForegroundColor = ConsoleColor.White;
	Console.BackgroundColor = ConsoleColor.Blue;
	Console.WriteLine("================================================================================");
	Console.WriteLine("                                  Flight Plan                                   ");
	Console.WriteLine("================================================================================");
	Console.ResetColor();
	Console.WriteLine();
	WriteFieldToConsole("Airline: ", $"{flightPlanMessage.Airline} - {airlines[flightPlanMessage.Airline].AirlineName}");
	WriteFieldToConsole("Flight Number: ", flightPlanMessage.FlightNumber);
	WriteFieldToConsole("Aircraft Type: ", $"{flightPlanMessage.AircraftType} - {aircraftTypes[flightPlanMessage.AircraftType].AircraftTypeName}");
	WriteFieldToConsole("Departure Airport: ", $"{flightPlanMessage.OriginAirport} - {originAirport.AirportName} ({originAirport.CityName}, {originAirport.CountryCode})");
	WriteFieldToConsole("Destination Airport: ", $"{flightPlanMessage.DestinationAirport} - {destinationAirport.AirportName} ({destinationAirport.CityName}, {destinationAirport.CountryCode})");
	WriteFieldToConsole("Distance: ", $"{CalculateDistance(originAirport.Coordinates(), destinationAirport.Coordinates()):N0} km");
	WriteFieldToConsole("Departure Time: ", $"{flightPlanMessage.DepartureTime:yyyy-MM-dd HH:mm:ss}");
	WriteFieldToConsole("Arrival Time: ", $"{flightPlanMessage.ArrivalTime:yyyy-MM-dd HH:mm:ss}");
	WriteFieldToConsole("Rotational Speed: ", $"{flightPlanMessage.RotationalSpeed:N0} km/h");
	WriteFieldToConsole("Initial Climb Altitude: ", $"{flightPlanMessage.InitialClimbAltitude:N0} meters");
	WriteFieldToConsole("Cruise Altitude: ", $"{flightPlanMessage.CruiseAltitude:N0} meters");
	WriteFieldToConsole("Start Descent Distance: ", $"{flightPlanMessage.StartDescentDistance:N0} km");
	WriteFieldToConsole("Start Approach Altitude: ", $"{flightPlanMessage.StartApproachAltitude:N0} meters");
	Console.WriteLine();
	Console.WriteLine();
	Console.WriteLine("Press any key to submit for approval...");
	Console.ReadKey(true);
}

void WriteFieldToConsole(string fieldName, string fieldValue)
{
	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.Write(fieldName);
	Console.ForegroundColor = ConsoleColor.Gray;
	Console.WriteLine(fieldValue);
}

