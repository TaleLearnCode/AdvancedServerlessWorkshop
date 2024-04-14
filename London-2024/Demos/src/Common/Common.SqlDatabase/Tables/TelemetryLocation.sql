CREATE TABLE dbo.TelemetryLocation
(
  TelemetryLocationId     BIGINT           NOT NULL IDENTITY(1,1),
  FlightTelemetryId       UNIQUEIDENTIFIER NOT NULL,
  Latitude                FLOAT                NULL,
  Longitude               FLOAT                NULL,
  DistanceSinceLastReport INT                  NULL,
  DistanceFromOrigin      INT                  NULL,
  DistanceToDestination   INT                  NULL,
  IsIncomplete            BIT                  NULL CONSTRAINT dfTelemetryLocation_IsIncomplete DEFAULT(0),
  CONSTRAINT pkcTelemetryLocation PRIMARY KEY (TelemetryLocationId),
  CONSTRAINT fkTelemetryLocation_Telemetry FOREIGN KEY (FlightTelemetryId) REFERENCES dbo.Telemetry (FlightTelemetryId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryLocation',                                                    @value = N'Table to store the location data for a flight.',                                                                  @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'TelemetryLocationId',              @value = N'Identifier of the location data for a flight.',                                                                   @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'FlightTelemetryId',                @value = N'Identifier of the Telemetry data for a flight.',                                                                  @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'Latitude',                         @value = N'The latitude of the aircraft at the time of the Telemetry data.',                                                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'Longitude',                        @value = N'The longitude of the aircraft at the time of the Telemetry data.',                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'DistanceSinceLastReport',          @value = N'The distance traveled in kilometers since the last Telemetry data.',                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'DistanceFromOrigin',               @value = N'The distance traveled in kilometers since the origin of the flight.',                                             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'DistanceToDestination',            @value = N'The distance remaining in kilometers to the destination of the flight.',                                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'IsIncomplete',                     @value = N'Indicates if the location data is incomplete.',                                                                   @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'pkcTelemetryLocation',             @value = N'Defines the primary key for the TelemetryLocation table using the TelemetryLocationId column.',                   @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'fkTelemetryLocation_Telemetry',    @value = N'Defines the relationship between the TelemetryLocation and Telemetry tables using the FlightTelemetryId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryLocation', @level2name = N'dfTelemetryLocation_IsIncomplete', @value = N'Defines the default value for the IsIncomplete field as 0 (false).',                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO