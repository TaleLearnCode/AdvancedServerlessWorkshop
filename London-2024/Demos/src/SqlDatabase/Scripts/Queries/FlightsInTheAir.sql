DECLARE @TrackingTime TIME = CONVERT(TIME, GETUTCDATE())

SELECT AirlineCode,
       FlightNumber,
       DepartureAirportCode,
       ArrivalAirportCode,
       DepartureTime,
       ArrivalTime,
       FlightTime,
       AircraftTypeCode,
       DATEDIFF(MINUTE, DepartureTime, @TrackingTime) AS CurrentFlightTime,
       (DATEDIFF(MINUTE, DepartureTime, @TrackingTime) * 100.0 / FlightTime) AS PercentageComplete
  FROM dbo.FlightSchedule
 WHERE DepartureTime <= @TrackingTime
   AND ArrivalTime >= @TrackingTime