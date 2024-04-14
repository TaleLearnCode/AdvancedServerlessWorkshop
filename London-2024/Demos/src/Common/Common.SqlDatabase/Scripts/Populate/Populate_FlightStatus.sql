MERGE dbo.FlightStatus AS TARGET
USING (VALUES ('010', 'Scheduled',                   'The flight is scheduled to depart at the specified time.'),
              ('020', 'Boarding Imminent',           'Passengers are advised to proceed to the gate as boarding is about to commence shortly.'),
              ('030', 'Boarding',                    'Passengers are invited to board the aircraft.'),
              ('040', 'Waiting for Departure',       'The aircraft is prepared for departure and awaiting clearance from air traffic control.'),
              ('050', 'Departed',                    'The aircraft has left the departure gate and is en route to its destination.'),
              ('060', 'Taxiing to Runway',           'The aircraft is in motion on the ground, heading towards the runway for takeoff.'),
              ('070', 'Waiting for Takeoff',         'The aircraft is in line on the runway, waiting for clearance to take off.'),
              ('080', 'Takeoff Phase',               'The aircraft is accelerating down the runway, preparing to lift off into the air.'),
              ('090', 'Initial Climb Phase',         'The aircraft has ascended from the runway and is beginning its climb to cruising altitude.'),
              ('100', 'Climbing to Cruise Altitude', 'The aircraft is continuing its ascent to reach its designated cruising altitude.'),
              ('110', 'In Cruise',                   'The aircraft has reached its cruising altitude and is flying steadily towards its destination.'),
              ('120', 'Commencing Descent',          'The aircraft is beginning its descent from cruising altitude towards the destination airport.'),
              ('130', 'Commencing Approach',         'The aircraft is initiating its approach towards the destination airport''s runway.'),
              ('140', 'Landing',                     'The aircraft is making contact with the runway and preparing to come to a stop.'),
              ('150', 'Landed',                      'The aircraft has landed and is taxiing towards the gate for passenger disembarkation.'),
              ('160', 'Taxiing to Gate',             'The aircraft has landed and is taxiing towards the gate for passenger disembarkation.'),
              ('170', 'Arrived at Gate',             'The aircraft has reached its designated gate and passengers are disembarking.'))
AS SOURCE (FlightStatusCode,
           FlightStatusName,
           FlightStatusDescription)
ON TARGET.FlightStatusCode = SOURCE.FlightStatusCode
WHEN MATCHED THEN UPDATE SET TARGET.FlightStatusName        = SOURCE.FlightStatusName,
                             TARGET.FlightStatusDescription = SOURCE.FlightStatusDescription
WHEN NOT MATCHED BY TARGET THEN INSERT (FlightStatusCode,
                                       FlightStatusName,
                                       FlightStatusDescription)
                               VALUES (SOURCE.FlightStatusCode,
                                       SOURCE.FlightStatusName,
                                       SOURCE.FlightStatusDescription)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO