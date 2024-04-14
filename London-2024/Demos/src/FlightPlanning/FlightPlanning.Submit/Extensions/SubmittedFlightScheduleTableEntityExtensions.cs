namespace TaleLearnCode.FlightTracker.SubmitFlightPlan.Extensions;

internal static class SubmittedFlightScheduleTableEntityExtensions
{
	public static FlightPlan ToFlightPlan(this SubmittedFlightScheduleTableEntity entity) =>
		new()
		{
			FlightPlanId = new Guid(entity.RowKey),
			AirlineCode = entity.Airline,
			FlightNumber = entity.FlightNumber,
			OriginAirportCode = entity.OriginAirport,
			DestinationAirportCode = entity.DestinationAirport,
			AircraftTypeCode = entity.AircraftType,
			DepartureTime = entity.DepartureTime,
			ArrivalTime = entity.ArrivalTime,
			Bearing = entity.Bearing,
			RotationalSpeed = entity.RotationSpeed,
			InitialClimbAltitude = entity.InitialClimbAltitude,
			CruiseAltitude = entity.CruiseAltitude,
			StartDescentDistance = entity.StartDescentDistance,
			StartApproachAltitude = entity.StartApproachAltitude,
			LandingSpeed = entity.LandingSpeed,
			CurrentFlightPhase = entity.CurrentFlightPhase
		};
}