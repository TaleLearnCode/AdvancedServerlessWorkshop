namespace TaleLearnCode.FlightTrackingDemo.Common.CosmosData.Models;

public class FlightPosition
{
	public decimal Longitude { get; set; }
	public decimal Latitude { get; set; }
	public int Altitude { get; set; }
	public int GroundSpeed { get; set; }
}