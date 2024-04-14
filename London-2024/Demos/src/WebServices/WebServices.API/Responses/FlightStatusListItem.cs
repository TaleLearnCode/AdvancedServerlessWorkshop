#nullable disable

namespace TaleLearnCode.FlightTrackingDemo.WebServices.API.Responses;

public class FlightStatusListItem
{
	public string Id { get; set; }
	public string AirlineCode { get; set; }
	public string AirlineName { get; set; }
	public string FlightNumber { get; set; }
	public DateTime DepartureTime { get; set; }
	public string DepartureAirportCode { get; set; }
	public string DepartureAirportName { get; set; }
	public DateTime ArrivalTime { get; set; }
	public string DestinationAirportCode { get; set; }
	public string DestinationAirportName { get; set; }
	public string FlightStatusCode { get; set; }
	public string FlightStatusName { get; set; }
	public string FlightStatusDescription { get; set; }
}