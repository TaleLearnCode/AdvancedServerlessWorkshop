JsonSerializerOptions _jsonSerializerOptions = new()
{
	PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
	DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
	WriteIndented = true
};

using HttpClient httpClient = new();

const string _transmitTelemetryUrl = " http://localhost:7081/ReceiveTelemetry";

FlightTrackerContext _context = new();
FlightPlan _flightPlan = await RetrieveFlightPlan(_context);
int _telemetryInterval = PromptForInt("Enter the telemetry interval in seconds: ") * 1000;
await SimulateFlightPlanAsync(_context, _flightPlan, _telemetryInterval);

void PrintBanner()
{
	Console.Clear();
	Console.ForegroundColor = ConsoleColor.White;
	Console.BackgroundColor = ConsoleColor.Magenta;
	Console.WriteLine(@"  _________.__              .__          __                ");
	Console.WriteLine(@" /   _____/|__| _____  __ __|  | _____ _/  |_  ___________ ");
	Console.WriteLine(@" \_____  \ |  |/     \|  |  \  | \__  \\   __\/  _ \_  __ \");
	Console.WriteLine(@" /        \|  |  Y Y  \  |  /  |__/ __ \|  | (  <_> )  | \/");
	Console.WriteLine(@"/_______  /|__|__|_|  /____/|____(____  /__|  \____/|__|   ");
	Console.WriteLine(@"        \/          \/                \/                   ");
	Console.ResetColor();
	Console.WriteLine();
}

//void PrintMenu()
//{
//	Console.ForegroundColor = ConsoleColor.White;
//	Console.BackgroundColor = ConsoleColor.DarkBlue;
//	Console.WriteLine("1. List all flights");
//	Console.WriteLine("2. List all flights for a specific aircraft");
//	Console.WriteLine("3. List all flights for a specific airport");
//	Console.WriteLine("4. List all flights for a specific airline");
//	Console.WriteLine("5. List all flights for a specific city");
//	Console.WriteLine("6. List all flights for a specific country");
//	Console.WriteLine("7. List all flights for a specific state");
//	Console.WriteLine("8. List all flights for a specific flight number");
//	Console.WriteLine("9. List all flights for a specific date");
//	Console.WriteLine("10. List all flights for a specific date range");
//	Console.WriteLine("11. Exit");
//	Console.WriteLine();
//	Console.Write("Enter your selection: ");
//}

async Task<FlightPlan> RetrieveFlightPlan(FlightTrackerContext context)
{
	FlightPlan? flightPlan = null;
	do
	{
		PrintBanner();
		flightPlan = await PromptForFlightPlanAsync(_context);
		DisplayFlightPlan(flightPlan);
		if (!PromptForBool("Would you like to simulate this flight plan? (Y/N)"))
			flightPlan = null;
	} while (flightPlan is null);
	Console.WriteLine();
	return flightPlan;
}


async Task<FlightPlan> PromptForFlightPlanAsync(FlightTrackerContext context)
{
	FlightPlan? flightPlan = null;
	while (flightPlan is null)
	{
		Console.Write("Enter the flight plan identifier: ");
		string? flightPlanIdentifier = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(flightPlanIdentifier))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Invalid flight plan identifier. Please try again.");
			Console.ForegroundColor = ConsoleColor.White;
		}
		else if (flightPlanIdentifier.Equals("LIST", StringComparison.InvariantCultureIgnoreCase))
		{
			await DisplayFlightPlanListAsync(context);
		}
		else
		{
			if (Guid.TryParse(flightPlanIdentifier, out Guid flightPlanId))
			{
				flightPlan = await context.FlightPlans
					.Include(x => x.AirlineCodeNavigation)
					.Include(x => x.OriginAirportCodeNavigation)
					.Include(x => x.DestinationAirportCodeNavigation)
					.Include(x => x.AircraftTypeCodeNavigation)
					.FirstOrDefaultAsync(f => f.FlightPlanId == flightPlanId);
				if (flightPlan == null)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Flight plan not found. Please try again.");
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Invalid flight plan identifier entered. Please try again.");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
	}
	return flightPlan;
}

void DisplayFlightPlan(FlightPlan flightPlan)
{
	Airport originAirport = flightPlan.OriginAirportCodeNavigation;
	Airport destinationAirport = flightPlan.DestinationAirportCodeNavigation;
	Console.WriteLine();
	Console.WriteLine();
	Console.ForegroundColor = ConsoleColor.White;
	Console.BackgroundColor = ConsoleColor.DarkGreen;
	Console.WriteLine("================================================================================");
	Console.WriteLine("                                  Flight Plan                                   ");
	Console.WriteLine("================================================================================");
	Console.ResetColor();
	Console.WriteLine();
	WriteFieldToConsole("Airline: ", $"{flightPlan.AirlineCode} - {flightPlan.AirlineCodeNavigation.AirlineName}");
	WriteFieldToConsole("Flight Number: ", $"{flightPlan.AirlineCode}-{flightPlan.FlightNumber}");
	WriteFieldToConsole("Aircraft Type: ", $"{flightPlan.AircraftTypeCode} - {flightPlan.AircraftTypeCodeNavigation.AircraftTypeName}");
	WriteFieldToConsole("Departure Airport: ", $"{originAirport.Iatacode} - {originAirport.AirportName} ({originAirport.CityName}, {originAirport.CountryCode})");
	WriteFieldToConsole("Destination Airport: ", $"{destinationAirport.Iatacode} - {destinationAirport.AirportName} ({destinationAirport.CityName}, {destinationAirport.CountryCode})");
	WriteFieldToConsole("Distance: ", $"{CalculateDistance(originAirport.Coordinates(), destinationAirport.Coordinates()):N0} km");
	WriteFieldToConsole("Departure Time: ", $"{flightPlan.DepartureTime:yyyy-MM-dd HH:mm:ss}");
	WriteFieldToConsole("Arrival Time: ", $"{flightPlan.ArrivalTime:yyyy-MM-dd HH:mm:ss}");
	WriteFieldToConsole("Rotational Speed: ", $"{flightPlan.RotationalSpeed:N0} km/h");
	WriteFieldToConsole("Initial Climb Altitude: ", $"{flightPlan.InitialClimbAltitude:N0} meters");
	WriteFieldToConsole("Cruise Altitude: ", $"{flightPlan.CruiseAltitude:N0} meters");
	WriteFieldToConsole("Start Descent Distance: ", $"{flightPlan.StartDescentDistance:N0} km");
	WriteFieldToConsole("Start Approach Altitude: ", $"{flightPlan.StartApproachAltitude:N0} meters");
	Console.WriteLine();
	Console.WriteLine();
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

async Task DisplayFlightPlanListAsync(FlightTrackerContext context)
{
	List<FlightPlan> flightPlans = await context.FlightPlans
		.Include(x => x.OriginAirportCodeNavigation)
		.Include(x => x.DestinationAirportCodeNavigation)
		.ToListAsync();
	Console.WriteLine();
	if (flightPlans.Count == 0)
	{
		Console.WriteLine("There are no flight plans available.");
	}
	else
	{
		Console.ForegroundColor = ConsoleColor.White;
		Console.BackgroundColor = ConsoleColor.DarkGreen;
		Console.WriteLine("================================================================================");
		Console.WriteLine("                               Flight Plans                                    ");
		Console.WriteLine("================================================================================");
		Console.ResetColor();
		Console.WriteLine();
		Console.WriteLine("Flight Plan Id                          Flight     Origin    Departure              Dest    Arrival");
		foreach (FlightPlan flightPlan in flightPlans)
			Console.WriteLine($"{flightPlan.FlightPlanId}    {flightPlan.AirlineCode}-{flightPlan.FlightNumber}    {flightPlan.OriginAirportCodeNavigation.Iatacode}       {flightPlan.DepartureTime:yyyy-MM-dd HH:mm:ss}    {flightPlan.DestinationAirportCodeNavigation.Iatacode}     {flightPlan.ArrivalTime:yyyy-MM-dd HH:mm:ss}");
		Console.WriteLine();

	}
}

async Task SimulateFlightPlanAsync(FlightTrackerContext context, FlightPlan flightPlan, int telemetryInterval)
{

	PrintBanner();
	PrintSimulatedFlightSummary(flightPlan, telemetryInterval);

	Dictionary<string, string> flightStatuses = context.FlightStatuses.ToDictionary(f => f.FlightStatusCode, f => f.FlightStatusName);

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
		flightTelemetry = GenerateTelemetryData(context, flightPlan, flightTelemetry, telemetryInterval, flightTimer, segmentCycles);

		// Output the telemetry data
		Console.WriteLine(flightTelemetry.LogEntry(flightPlan, flightStatuses, flightTimer));

		// Transmit telemetry data
		await TransmitTelemetry(flightTelemetry, flightPlan);

		segmentCycles++;

		Thread.Sleep(telemetryInterval);

	}

}

void PrintSimulatedFlightSummary(FlightPlan flightPlan, int telemetryInterval)
{
	Airport originAirport = flightPlan.OriginAirportCodeNavigation;
	Airport destinationAirport = flightPlan.DestinationAirportCodeNavigation;
	TimeSpan flightDuration = flightPlan.ArrivalTime - flightPlan.DepartureTime;
	Console.WriteLine("Starting flight simulation");
	Console.WriteLine();
	WriteFieldToConsole("Flight Number: ", $"{flightPlan.AirlineCode}-{flightPlan.FlightNumber}");
	WriteFieldToConsole("Aircraft Type: ", $"{flightPlan.AircraftTypeCode} - {flightPlan.AircraftTypeCodeNavigation.AircraftTypeName}");
	WriteFieldToConsole("Departure Airport: ", $"{originAirport.Iatacode} - {originAirport.AirportName} ({originAirport.CityName}, {originAirport.CountryCode})");
	WriteFieldToConsole("Destination Airport: ", $"{destinationAirport.Iatacode} - {destinationAirport.AirportName} ({destinationAirport.CityName}, {destinationAirport.CountryCode})");
	WriteFieldToConsole("Distance: ", $"{CalculateDistance(originAirport.Coordinates(), destinationAirport.Coordinates()):N0} km");
	WriteFieldToConsole("Flight Duration: ", $"{flightDuration.Hours:N0} hours; {flightDuration.Minutes:N0} minutes");
	WriteFieldToConsole("Telemetry Cycles: ", ((int)Math.Ceiling(flightDuration.TotalSeconds / (telemetryInterval / 1000))).ToString("#,##0"));
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

FlightTelemetry GenerateTelemetryData(
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

	//await context.FlightTelemetries.AddAsync(flightTelemetry);
	//await context.SaveChangesAsync();

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

double CalculateNextLongitude(double longitude, double bearing, int groundSpeed, int timeInterval) => longitude + DistanceMoved(groundSpeed, timeInterval) * Math.Sin(RadianBearing(bearing));

double CalculateNextLatitude(double latitude, double bearing, int groundSpeed, int timeInterval) => latitude + DistanceMoved(groundSpeed, timeInterval) * Math.Cos(RadianBearing(bearing));

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

int GenerateRandomValue(int minValue, int maxValue) => (maxValue <= minValue) ? minValue : new Random().Next(minValue, maxValue + 1);

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

double DistanceMoved(int groundSpeedPerHour, int timeInterval) => GroundSpeedPerTimeInterval(groundSpeedPerHour, timeInterval) / WeightsAndMeasurements.DistanceConversionFactor;

double GroundSpeedPerTimeInterval(int groundSpeedPerHour, int timeInterval) => (double)groundSpeedPerHour / 60 * timeInterval / 60000;

double RadianBearing(double bearing) => bearing * (Math.PI / 180); // Convert bearing to radians

async Task TransmitTelemetry(FlightTelemetry telemetry, FlightPlan flightPlan)
{
	TelemetryMessage telemetryMessage = telemetry.ToTelemetryMessage(flightPlan);
	StringContent content = new(JsonSerializer.Serialize(telemetryMessage, _jsonSerializerOptions), Encoding.UTF8, "application/json");
	HttpResponseMessage response = await httpClient.PostAsync(_transmitTelemetryUrl, content);
	if (!response.IsSuccessStatusCode)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"Error transmitting telemetry data: {response.ReasonPhrase}");
		Console.ForegroundColor = ConsoleColor.White;
	}
}