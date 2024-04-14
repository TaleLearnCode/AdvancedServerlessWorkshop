CREATE TABLE dbo.FlightTelemetry
(
  FlightTelemetryId     UNIQUEIDENTIFIER NOT NULL,
  FlightPlanId          UNIQUEIDENTIFIER NOT NULL,
  TelemetryTimestamp    DATETIME         NOT NULL,
  FlightStatusCode      CHAR(3)          NOT NULL,
  Longitude             DECIMAL(9,6)     NOT NULL,
  Latitude              DECIMAL(9,6)     NOT NULL,
  Altitude              INT              NOT NULL,
  GroundSpeed           INT              NOT NULL,
  FlightDuration        INT              NOT NULL,
  DistanceSinceLast     FLOAT            NOT NULL,
  DistanceSinceOrigin   FLOAT            NOT NULL,
  DistanceToDestination FLOAT            NOT NULL,
  PhaseSequence         INT              NOT NULL,
  CONSTRAINT pkcFlightTelemetry PRIMARY KEY (FlightTelemetryId),
  CONSTRAINT fkFlightTelemetry_FlightPlan FOREIGN KEY (FlightPlanId) REFERENCES dbo.FlightPlan (FlightPlanId),
  CONSTRAINT fkFlightTelemetry_FlightStatus FOREIGN KEY (FlightStatusCode) REFERENCES dbo.FlightStatus (FlightStatusCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'FlightTelemetry',                                                     @value = N'Table to store the Telemetry data for a flight.',                                                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'FlightTelemetryId',                 @value = N'Identifier of the Telemetry data for a flight.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'FlightPlanId',                      @value = N'Identifier of the Flight Plan for the Telemetry data.',                                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'TelemetryTimestamp',                @value = N'Timestamp of the Telemetry data.',                                                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'FlightStatusCode',                  @value = N'The status of the flight at the time of the Telemetry data.',                                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'Longitude',                         @value = N'The longitude of the aircraft at the time of the Telemetry data.',                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'Latitude',                          @value = N'The latitude of the aircraft at the time of the Telemetry data.',                                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'Altitude',                          @value = N'The altitude of the aircraft at the time of the Telemetry data.',                                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'GroundSpeed',                       @value = N'The speed of the aircraft relative to the ground at the time of the Telemetry data.',                                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'FlightDuration',                    @value = N'The duration of the flight in milliseconds at the time of the Telemetry data.',                                       @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'DistanceSinceLast',                 @value = N'The distance traveled in kilometers since the last Telemetry data.',                                                  @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'DistanceSinceOrigin',               @value = N'The distance traveled in kilometers since the origin of the flight.',                                                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'DistanceToDestination',             @value = N'The distance remaining in kilometers to the destination of the flight.',                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'PhaseSequence',                     @value = N'The sequential count of data recordings within the flight phase.',                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'pkcFlightTelemetry',                @value = N'Defines the primary key for the FlightTelemetry table using the FlightTelemetryId column.',                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'fkFlightTelemetry_FlightPlan',       @value = N'Defines the relationship between the FlightTelemetry and FlightPlan tables using the FlightPlanId column.',          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightTelemetry', @level2name = N'fkFlightTelemetry_FlightStatus',    @value = N'Defines the relationship between the FlightTelemetry and FlightStatus tables using the FlightStatusCode column.',     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO