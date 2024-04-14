using Microsoft.Azure.Cosmos;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaleLearnCode.FlightTrackingDemo.Common.CosmosData;
using TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;
using TaleLearnCode.FlightTrackingDemo.WebServices.API.Responses;

WebApplicationBuilder _builder = WebApplication.CreateBuilder(args);

_builder.Services.AddEndpointsApiExplorer();
_builder.Services.AddSwaggerGen();

WebApplication app = _builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

CosmosClient _cosmosClient = new(
	_builder.Configuration.GetConnectionString("Cosmos"),
	new CosmosClientOptions
	{
		Serializer = new CosmosSystemTextJsonSerializer(new JsonSerializerOptions
		{
			Converters = { new JsonStringEnumConverter() },
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true
		})
	});
Database _cosmosDatabase = _cosmosClient.GetDatabase(_builder.Configuration.GetValue<string>("CosmosSettings:DatabaseName"));
Container _cosmosContainer = _cosmosDatabase.GetContainer(_builder.Configuration.GetValue<string>("CosmosSettings:ContainerName"));

app.MapGet("/flightStatus/{airline}/{flightNumber}/{flightDate}", GetFlightStatus)
	.WithName("GetFlightStatus")
	.WithSummary("GetFlightStatus")
	.WithDescription("Retrieve the latest status for the specified flight.")
	.WithMetadata("airline", "The IATA code of the airline to search for.")
	.WithMetadata("flightNumber", "The flight number to search for.")
	.WithMetadata("flightDate", "The date of the flight to search for.")
	.WithOpenApi();

app.MapGet("/flightStatuses/{airline}/{origin}/{destination}/{flightDate}", GetFlightStatuses)
	.WithName("GetFlightStatuses")
	.WithSummary("GetFlightStatuses")
	.WithDescription("Retrieve all flights for the specified airline, origin, destination, and date.")
	.WithMetadata("airline", "The IATA code of the airline to search for.")
	.WithMetadata("origin", "The IATA code of the origin airport to search for.")
	.WithMetadata("destination", "The IATA code of the destination airport to search for.")
	.WithMetadata("flightDate", "The date of the flights to search for.")
	.WithOpenApi();


app.Run();

async Task<IResult> GetFlightStatus(string airline, string flightNumber, DateTime flightDate)
{
	string id = $"{flightDate:yyyyMMdd}{airline}{flightNumber}";
	ItemResponse<CustomerFlightStatus> response = await _cosmosContainer.ReadItemAsync<CustomerFlightStatus>(id, new PartitionKey(airline));
	if (response.StatusCode == HttpStatusCode.OK)
		return TypedResults.Ok(response.Resource);
	else if (response.StatusCode == HttpStatusCode.NotFound)
		return Results.NotFound();
	else if (response.StatusCode == HttpStatusCode.BadRequest)
		return Results.BadRequest();
	else
		return Results.StatusCode((int)response.StatusCode);
}

async Task<IResult> GetFlightStatuses(string airline, string origin, string destination, DateTime flightDate)
{
	var query = new QueryDefinition("SELECT * FROM c WHERE c.airlineCode = @airlineCode AND c.originAirportCode = @origin AND c.destinationAirportCode = @destination AND c.flightDate = @flightDate")
		.WithParameter("@airlineCode", airline)
		.WithParameter("@origin", origin)
		.WithParameter("@destination", destination)
		.WithParameter("@flightDate", flightDate.ToString("yyyy-MM-dd"));
	using FeedIterator<CustomerFlightStatus> feed = _cosmosContainer.GetItemQueryIterator<CustomerFlightStatus>(query);
	List<FlightStatusListItem> results = [];
	while (feed.HasMoreResults)
	{
		FeedResponse<CustomerFlightStatus> response = await feed.ReadNextAsync();
		foreach (CustomerFlightStatus item in response)
		{
			results.Add(new()
			{
				Id = item.Id,
				AirlineCode = item.AirlineCode,
				AirlineName = item.Airline.Name,
				FlightNumber = item.FlightNumber,
				DepartureTime = item.DepartureTime,
				DepartureAirportCode = item.OriginAirportCode,
				DepartureAirportName = item.OriginAirport.Name,
				ArrivalTime = item.ArrivalTime,
				DestinationAirportCode = item.DestinationAirportCode,
				DestinationAirportName = item.DestinationAirport.Name,
				FlightStatusCode = item.FlightStatusCode,
				FlightStatusName = item.FlightStatus.Name,
				FlightStatusDescription = item.FlightStatus.Description
			});
		}
	}
	return TypedResults.Ok(results);
}