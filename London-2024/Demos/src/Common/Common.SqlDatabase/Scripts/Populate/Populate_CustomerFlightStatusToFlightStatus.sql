MERGE dbo.CustomerFlightStatusToFlightStatus AS TARGET
USING (VALUES ( 1, '010', '010'),
              ( 2, '010', '020'),
              ( 3, '020', '030'),
              ( 4, '020', '040'),
              ( 5, '030', '050'),
              ( 6, '040', '060'),
              ( 7, '040', '070'),
              ( 8, '050', '080'),
              ( 9, '050', '090'),
              (10, '060', '100'),
              (11, '070', '110'),
              (12, '080', '120'),
              (13, '090', '130'),
              (14, '080', '140'),
              (15, '090', '150'),
              (16, '090', '160'),
              (17, '100', '170'))
AS SOURCE (CustomerFlightStatusToFlightStatusId,
           CustomerFlightStatusCode,
           FlightStatusCode)
ON TARGET.CustomerFlightStatusToFlightStatusId = SOURCE.CustomerFlightStatusToFlightStatusId
WHEN MATCHED THEN UPDATE SET TARGET.CustomerFlightStatusCode = SOURCE.CustomerFlightStatusCode,
                             TARGET.FlightStatusCode         = SOURCE.FlightStatusCode
WHEN NOT MATCHED BY TARGET THEN INSERT (CustomerFlightStatusToFlightStatusId,
                                       CustomerFlightStatusCode,
                                       FlightStatusCode)
                               VALUES (SOURCE.CustomerFlightStatusToFlightStatusId,
                                       SOURCE.CustomerFlightStatusCode,
                                       SOURCE.FlightStatusCode)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO