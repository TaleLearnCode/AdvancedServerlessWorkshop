MERGE dbo.CustomerFlightStatus AS TARGET
USING (VALUES ('010', '010', 'Scheduled',  'Your flight is scheduled to depart at the specified time. Please arrive at the airport according to your check-in and boarding instructions.'),
              ('020', '030', 'Boarding',   'Boarding for your flight has begun. Please proceed to the gate and have your boarding pass ready for scanning.'),
              ('030', '050', 'Departed',   'Your flight has departed from the departure airport and is en route to its destination.'),
              ('040', '060', 'Taxing',     'The aircraft is currently taxiing from the gate to the runway in preparation for takeoff. Please remain seated and fasten your seatbelt as we prepare for departure.'),
              ('050', '080', 'Taking Off', 'The aircraft is accelerating down the runway, preparing to lift off into the air. Please ensure your seatbelt is securely fastened and tray tables are stowed.'),
              ('060', '100', 'Climbing',   'The aircraft is ascending to reach its designated cruising altitude. We''re climbing higher to provide you with a smooth and comfortable flight experience.'),
              ('070', '110', 'In Flight',  'Your flight is currently in progress. Sit back, relax, and enjoy your journey.'),
              ('080', '120', 'Descending', 'The aircraft is beginning its descent towards the destination airport. We''re preparing to land, so please return your seatbacks to their upright position and ensure all belongings are stowed securely.'),
              ('090', '150', 'Landed',     'Your flight has landed safely at the destination airport.'),
              ('100', '170', 'Arrived',    'You have arrived at your destination. Please gather your belongings and proceed to the arrival area. Welcome to your destination!'))
AS SOURCE (CustomerFlightStatusCode,
           CustomerFlightStatusName,
           CustomerFlightStatusDescription,
           AssociatedFlightStatusCode)
ON TARGET.CustomerFlightStatusCode = SOURCE.CustomerFlightStatusCode
WHEN MATCHED THEN UPDATE SET TARGET.CustomerFlightStatusName        = SOURCE.CustomerFlightStatusName,
                             TARGET.CustomerFlightStatusDescription = SOURCE.CustomerFlightStatusDescription,
                             TARGET.AssociatedFlightStatusCode      = SOURCE.AssociatedFlightStatusCode
WHEN NOT MATCHED BY TARGET THEN INSERT (CustomerFlightStatusCode,
                                       CustomerFlightStatusName,
                                       CustomerFlightStatusDescription,
                                       AssociatedFlightStatusCode)
                               VALUES (SOURCE.CustomerFlightStatusCode,
                                       SOURCE.CustomerFlightStatusName,
                                       SOURCE.CustomerFlightStatusDescription,
                                       SOURCE.AssociatedFlightStatusCode)
WHEN NOT MATCHED BY SOURCE THEN DELETE;