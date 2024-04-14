using Microsoft.EntityFrameworkCore;
using Prototype3;
using Prototype3.Data.StaticValues;
using Prototype3.Extensions;
using Prototype3.Models;
using System.Diagnostics;

FlightTrackerContext context = new();
Dictionary<string, Airport> airports = context.Airports.ToDictionary(a => a.Iatacode, a => a);
Dictionary<string, AircraftType> aircraftTypes = context.AircraftTypes.ToDictionary(a => a.AircraftTypeCode, a => a);
Dictionary<string, Airline> airlines = context.Airlines.ToDictionary(a => a.Iatacode, a => a);
Dictionary<string, string> flightStatuses = context.FlightStatuses.ToDictionary(f => f.FlightStatusCode, f => f.FlightStatusName);

PrintBanner();
Console.WriteLine();

string airlineCode = PromptForAirline();
string flightNumber = PromptForFlightNumber();
string originAirportCode = PromptForAirport("Enter the departure airport code: ");
string destinationAirportCode = PromptForAirport("Enter the arrival airport code: ");
string AircraftTypeCode = PromptForAircraftType();
int telemetryInterval = PromptForInt("Enter the telemetry interval in seconds: ") * 1000;

FlightPlan flightPlan = await CreateFlightPlanAsync(context, airlineCode, flightNumber, originAirportCode, destinationAirportCode, AircraftTypeCode);

int segmentCycles = 0;
FlightTelemetry flightTelemetry = flightPlan.InitialFlightTelemetry();
Stopwatch flightTimer = new();
while (flightPlan.CurrentFlightPhase != FlightPhases.Arrived)
{

	// Get the current flight phase
	string newFlightPhase = DetermineFlightStatus(flightPlan, flightTelemetry, segmentCycles);
	if (newFlightPhase != flightPlan.CurrentFlightPhase)
	{
		flightPlan.CurrentFlightPhase = newFlightPhase;
		await context.SaveChangesAsync();
		segmentCycles = 0;
	}

	// Be sure to time flight phases that are included in the flight time
	if (IsFlightPhaseTimeIncludedInFlightTime(flightPlan.CurrentFlightPhase) && !flightTimer.IsRunning)
		flightTimer.Start();
	if (!IsFlightPhaseTimeIncludedInFlightTime(flightPlan.CurrentFlightPhase) && flightTimer.IsRunning)
		flightTimer.Stop();

	// Generate telemetry data
	flightTelemetry = await GenerateTelemetryData(context, flightPlan, flightTelemetry, telemetryInterval, flightTimer, segmentCycles);

	// Output the telemetry data
	Console.WriteLine(flightTelemetry.LogEntry(flightPlan, flightStatuses, flightTimer));

	segmentCycles++;

	Thread.Sleep(telemetryInterval);

}



void PrintBanner()
{
	Console.Clear();
	Console.ForegroundColor = ConsoleColor.White;
	Console.BackgroundColor = ConsoleColor.Blue;
	Console.WriteLine(@"___________.__  .__       .__     __    ___________                     __                 ");
	Console.WriteLine(@"\_   _____/|  | |__| ____ |  |___/  |_  \__    ___/___________    ____ |  | __ ___________ ");
	Console.WriteLine(@" |    __)  |  | |  |/ ___\|  |  \   __\   |    |  \_  __ \__  \ _/ ___\|  |/ // __ \_  __ \");
	Console.WriteLine(@" |     \   |  |_|  / /_/  >   Y  \  |     |    |   |  | \// __ \\  \___|    <\  ___/|  | \/");
	Console.WriteLine(@" \___  /   |____/__\___  /|___|  /__|     |____|   |__|  (____  /\___  >__|_ \\___  >__|   ");
	Console.WriteLine(@"     \/           /_____/      \/                             \/     \/     \/    \/       ");
	Console.ResetColor();
	Console.WriteLine();
}

void PrintAirports()
{
	Console.WriteLine();
	Console.WriteLine("Airports");
	Console.WriteLine("========");
	foreach (Airport airport in context.Airports)
		Console.WriteLine($"{airport.Iatacode} - {airport.AirportName} ({airport.CityName}, {airport.CountryCode})");
}

void PrintAircraftTypes()
{
	Console.WriteLine();
	Console.WriteLine("Aircraft Types");
	Console.WriteLine("==============");
	foreach (AircraftType aircraftType in context.AircraftTypes)
		Console.WriteLine($"{aircraftType.AircraftTypeCode} - {aircraftType.AircraftTypeName}");
}

void PrintAirlines()
{
	Console.WriteLine();
	Console.WriteLine("Airlines");
	Console.WriteLine("========");
	foreach (Airline airline in context.Airlines)
		Console.WriteLine($"{airline.Iatacode} - {airline.AirlineName}");
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

async Task<FlightPlan> CreateFlightPlanAsync(
	FlightTrackerContext context,
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

	FlightPlan flightPlan = new()
	{
		AirlineCode = airlineCode,
		FlightNumber = flightNumber,
		OriginAirportCode = originAirportCode,
		DestinationAirportCode = destinationAirportCode,
		AircraftTypeCode = aircraftTypeCode,
		DepartureTime = departureTime,
		ArrivalTime = CalculateArrivalTime(originCoordinates, destinationCoordinates, departureTime, aircraftType.CruiseSpeedLower),
		RotationalSpeed = GenerateRandomValue(aircraftType.RotationalSpeedLower, aircraftType.RotationalSpeedUpper),
		InitialClimbAltitude = GenerateRandomValue(aircraftType.InitialClimbAltitudeLower, aircraftType.InitialClimbAltitudeUpper),
		CruiseAltitude = GenerateRandomValue(aircraftType.CruiseAltitudeLower, aircraftType.CruiseAltitudeUpper),
		StartDescentDistance = GenerateRandomValue(WeightsAndMeasurements.DescentRangeLower, WeightsAndMeasurements.DescentRangeUpper),
		StartApproachAltitude = GenerateRandomValue(WeightsAndMeasurements.ApproachAltitudeLower, WeightsAndMeasurements.ApproachAltitudeUpper),
		CurrentFlightPhase = FlightPhases.Scheduled
	};

	await context.FlightPlans.AddAsync(flightPlan);
	await context.SaveChangesAsync();

	Console.WriteLine();
	Console.WriteLine();
	Console.ForegroundColor = ConsoleColor.White;
	Console.BackgroundColor = ConsoleColor.Blue;
	Console.WriteLine("================================================================================");
	Console.WriteLine("                                  Flight Plan                                   ");
	Console.WriteLine("================================================================================");
	Console.ResetColor();
	Console.WriteLine();
	WriteFieldToConsole("Airline: ", $"{airlineCode} - {airlines[airlineCode].AirlineName}");
	WriteFieldToConsole("Flight Number: ", flightNumber);
	WriteFieldToConsole("Aircraft Type: ", $"{aircraftTypeCode} - {aircraftType.AircraftTypeName}");
	WriteFieldToConsole("Departure Airport: ", $"{originAirportCode} - {originAirport.AirportName} ({originAirport.CityName}, {originAirport.CountryCode})");
	WriteFieldToConsole("Destination Airport: ", $"{destinationAirportCode} - {destinationAirport.AirportName} ({destinationAirport.CityName}, {destinationAirport.CountryCode})");
	WriteFieldToConsole("Distance: ", $"{distance:N0} km");
	WriteFieldToConsole("Departure Time: ", $"{flightPlan.DepartureTime:yyyy-MM-dd HH:mm:ss}");
	WriteFieldToConsole("Arrival Time: ", $"{flightPlan.ArrivalTime:yyyy-MM-dd HH:mm:ss}");
	WriteFieldToConsole("Rotational Speed: ", $"{flightPlan.RotationalSpeed:N0} km/h");
	WriteFieldToConsole("Initial Climb Altitude: ", $"{flightPlan.InitialClimbAltitude:N0} meters");
	WriteFieldToConsole("Cruise Altitude: ", $"{flightPlan.CruiseAltitude:N0} meters");
	WriteFieldToConsole("Start Descent Distance: ", $"{flightPlan.StartDescentDistance:N0} km");
	WriteFieldToConsole("Start Approach Altitude: ", $"{flightPlan.StartApproachAltitude:N0} meters");
	Console.WriteLine();
	Console.WriteLine();
	Console.WriteLine("Press any key to continue...");
	Console.ReadKey(true);

	return await context.FlightPlans
		.Include(x => x.AircraftTypeCodeNavigation)
		.Include(x => x.OriginAirportCodeNavigation)
		.Include(x => x.DestinationAirportCodeNavigation)
		.FirstOrDefaultAsync(x => x.FlightPlanId == flightPlan.FlightPlanId)
		?? throw new Exception("Failed to retrieved the flight plan.");

}

void WriteFieldToConsole(string fieldName, string fieldValue)
{
	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.Write(fieldName);
	Console.ForegroundColor = ConsoleColor.Gray;
	Console.WriteLine(fieldValue);
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

static double CalculateBearing(
	double originLatitude,
	double originLongitude,
	double destinationLatitude,
	double destinationLongitude)
{
	double dLon = destinationLongitude - originLongitude;
	double y = Math.Sin(dLon) * Math.Cos(destinationLatitude);
	double x = Math.Cos(originLatitude) * Math.Sin(destinationLatitude) - Math.Sin(originLatitude) * Math.Cos(destinationLatitude) * Math.Cos(dLon);
	double bearing = Math.Atan2(y, x);
	bearing *= (180 / Math.PI);
	bearing = (bearing + 360) % 360;
	return bearing;
}

int GenerateRandomValue(int minValue, int maxValue) => (maxValue <= minValue) ? minValue : new Random().Next(minValue, maxValue + 1);

double CalculateNextLatitude(double latitude, double bearing, int groundSpeed, int timeInterval) => latitude + DistanceMoved(groundSpeed, timeInterval) * Math.Cos(RadianBearing(bearing));

double CalculateNextLongitude(double longitude, double bearing, int groundSpeed, int timeInterval) => longitude + DistanceMoved(groundSpeed, timeInterval) * Math.Sin(RadianBearing(bearing));

double RadianBearing(double bearing) => bearing * (Math.PI / 180); // Convert bearing to radians

double DistanceMoved(int groundSpeedPerHour, int timeInterval) => GroundSpeedPerTimeInterval(groundSpeedPerHour, timeInterval) / WeightsAndMeasurements.DistanceConversionFactor;

double GroundSpeedPerTimeInterval(int groundSpeedPerHour, int timeInterval) => (double)groundSpeedPerHour / 60 * timeInterval / 60000;

double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

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

string DetermineFlightStatus(
	FlightPlan flightPlan,
	FlightTelemetry previousFlightTelemetry,
	int segmentCycles)
{
	if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Scheduled && segmentCycles > FlightSegmentCycles.ScheduledCycles)
		return FlightPhases.BoardingImminent;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.BoardingImminent && segmentCycles > FlightSegmentCycles.BoardingImminentCycles)
		return FlightPhases.Boarding;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Boarding && segmentCycles > FlightSegmentCycles.BoardingCycles)
		return FlightPhases.WaitingForDeparture;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.WaitingForDeparture && segmentCycles > FlightSegmentCycles.WaitingForDepartureCycles)
		return FlightPhases.Departed;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Departed && segmentCycles > FlightSegmentCycles.DepartedCycles)
		return FlightPhases.TaxingToRunway;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.TaxingToRunway && segmentCycles > FlightSegmentCycles.TaxingToRunwayCycles)
		return FlightPhases.Takeoff;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Takeoff && segmentCycles > FlightSegmentCycles.TakeoffCycles)
		return FlightPhases.InitialClimb;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.InitialClimb && previousFlightTelemetry.Altitude >= flightPlan.InitialClimbAltitude)
		return FlightPhases.Climbing;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Climbing && previousFlightTelemetry.Altitude >= flightPlan.CruiseAltitude)
		return FlightPhases.Cruise;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Cruise && CalculateDistance(previousFlightTelemetry.Coordinates(), flightPlan.DestinationCoordinates()) <= flightPlan.StartDescentDistance)
		return FlightPhases.Descent;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Descent && previousFlightTelemetry.Altitude <= flightPlan.StartApproachAltitude)
		return FlightPhases.Approach;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Approach && previousFlightTelemetry.Altitude <= flightPlan.DestinationAirportCodeNavigation.Elevation)
		return FlightPhases.Landing;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Landing && segmentCycles > FlightSegmentCycles.LandingCycles)
		return FlightPhases.Landed;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.Landed && segmentCycles > FlightSegmentCycles.LandedCycles)
		return FlightPhases.TaxiingToGate;
	else if (previousFlightTelemetry.FlightStatusCode == FlightPhases.TaxiingToGate && segmentCycles > FlightSegmentCycles.TaxiingToGateCycles)
		return FlightPhases.Arrived;
	else
		return previousFlightTelemetry.FlightStatusCode;
}

bool IsFlightPhaseTimeIncludedInFlightTime(string flightPhase) => flightPhase switch
{
	FlightPhases.Scheduled => false,
	FlightPhases.BoardingImminent => false,
	FlightPhases.Boarding => false,
	FlightPhases.WaitingForDeparture => false,
	FlightPhases.Departed => false,
	FlightPhases.TaxingToRunway => false,
	FlightPhases.WaitingForTakeoff => false,
	FlightPhases.Takeoff => false,
	FlightPhases.InitialClimb => true,
	FlightPhases.Climbing => true,
	FlightPhases.Cruise => true,
	FlightPhases.Descent => true,
	FlightPhases.Approach => true,
	FlightPhases.Landing => true,
	FlightPhases.Landed => false,
	FlightPhases.Arrived => false,
	_ => false
};

async Task<FlightTelemetry> GenerateTelemetryData(
	FlightTrackerContext context,
	FlightPlan flightPlan,
	FlightTelemetry previousTelemetry,
	int timeInterval,
	Stopwatch flightTimer,
	int segmentCycles)
{
	int groundSpeed = DetermineGroundSpeed(flightPlan, segmentCycles, previousTelemetry);
	double longitude = CalculateNextLongitude((double)previousTelemetry.Longitude, flightPlan.Bearing, groundSpeed, timeInterval);
	double latitude = CalculateNextLatitude((double)previousTelemetry.Latitude, flightPlan.Bearing, groundSpeed, timeInterval);
	Geocoordinate coordinates = new(latitude, longitude);

	FlightTelemetry flightTelemetry = new()
	{
		FlightPlanId = flightPlan.FlightPlanId,
		TelemetryTimestamp = DateTime.UtcNow,
		FlightStatusCode = flightPlan.CurrentFlightPhase,
		Longitude = (decimal)longitude,
		Latitude = (decimal)latitude,
		Altitude = previousTelemetry.Altitude + DetermineAltitudeChange(flightPlan, timeInterval),
		GroundSpeed = groundSpeed,
		FlightDuration = (int)flightTimer.Elapsed.TotalMilliseconds,
		DistanceSinceLast = CalculateDistance(previousTelemetry.Coordinates(), coordinates),
		DistanceSinceOrigin = CalculateDistance(flightPlan.OriginCoordinates(), coordinates),
		DistanceToDestination = CalculateDistance(coordinates, flightPlan.DestinationCoordinates()),
		PhaseSequence = segmentCycles
	};

	await context.FlightTelemetries.AddAsync(flightTelemetry);
	await context.SaveChangesAsync();

	return flightTelemetry;

}

int DetermineGroundSpeed(FlightPlan flightPlan, int segmentCycles, FlightTelemetry previousTelemetry) => flightPlan.CurrentFlightPhase switch
{
	FlightPhases.TaxingToRunway => flightPlan.AircraftTypeCodeNavigation.TaxiSpeed,
	FlightPhases.Takeoff => (flightPlan.RotationalSpeed / FlightSegmentCycles.TakeoffCycles) * segmentCycles,
	FlightPhases.InitialClimb => GenerateRandomValue(flightPlan.AircraftTypeCodeNavigation.InitialClimbSpeedLower, flightPlan.AircraftTypeCodeNavigation.InitialClimbSpeedUpper),
	FlightPhases.Climbing => GenerateRandomValue(flightPlan.AircraftTypeCodeNavigation.ClimbSpeedLower, flightPlan.AircraftTypeCodeNavigation.ClimbSpeedUpper),
	FlightPhases.Cruise => DetermineCruiseGroundSpeed(flightPlan, previousTelemetry),
	FlightPhases.Descent => GenerateRandomValue(flightPlan.AircraftTypeCodeNavigation.DescentSpeedLower, flightPlan.AircraftTypeCodeNavigation.DescentSpeedUpper),
	FlightPhases.Approach => GenerateRandomValue(flightPlan.AircraftTypeCodeNavigation.ApproachSpeedLower, flightPlan.AircraftTypeCodeNavigation.ApproachSpeedUpper),
	FlightPhases.Landing => previousTelemetry.GroundSpeed - (previousTelemetry.GroundSpeed / FlightSegmentCycles.LandingCycles) + flightPlan.AircraftTypeCodeNavigation.TaxiSpeed,
	FlightPhases.Landed => flightPlan.AircraftTypeCodeNavigation.TaxiSpeed,
	FlightPhases.TaxiingToGate => flightPlan.AircraftTypeCodeNavigation.TaxiSpeed,
	_ => 0,
};

int DetermineCruiseGroundSpeed(FlightPlan flightPlan, FlightTelemetry previousTelemetry)
{

	int previousGroundSpeed = previousTelemetry.GroundSpeed;
	int cruiseSpeedLower = flightPlan.AircraftTypeCodeNavigation.CruiseSpeedLower;
	int cruiseSpeedUpper = flightPlan.AircraftTypeCodeNavigation.CruiseSpeedUpper;
	int maxDifference = (int)(previousGroundSpeed * 0.1); // 10% difference

	// Calculate the range for the cruise speed
	int lowerRange = Math.Max(cruiseSpeedLower, previousGroundSpeed - maxDifference);
	int upperRange = Math.Min(cruiseSpeedUpper, previousGroundSpeed + maxDifference);

	return GenerateRandomValue(lowerRange, upperRange);

}

int DetermineAltitudeChange(FlightPlan flightPlan, int timeInterval)
{
	int altitudeChange = 0;

	switch (flightPlan.CurrentFlightPhase)
	{
		case FlightPhases.InitialClimb:
			altitudeChange = GenerateRandomValue(WeightsAndMeasurements.InitialClimbRateOfAscentLower, WeightsAndMeasurements.InitialClimbRateOfAscentUpper);
			break;
		case FlightPhases.Climbing:
			altitudeChange = GenerateRandomValue(WeightsAndMeasurements.ClimbRateOfAscentLower, WeightsAndMeasurements.ClimbRateOfAscentUpper);
			break;
		case FlightPhases.Descent:
			altitudeChange = GenerateRandomValue(WeightsAndMeasurements.DecentRateLower, WeightsAndMeasurements.DecentRateUpper);
			break;
		case FlightPhases.Approach:
			altitudeChange = GenerateRandomValue(WeightsAndMeasurements.ApproachRateOfDescentLower, WeightsAndMeasurements.ApproachRateOfDescentUpper);
			break;
	}

	// Adjust altitude change based on time interval
	double timeInMinutes = timeInterval / 60000.0; // Convert timeInterval to minutes
	altitudeChange = (int)(altitudeChange * timeInMinutes);

	return altitudeChange;
}
