CREATE TABLE dbo.TelemetryFlightParameter
(
  TelemetryFlightParameterId BIGINT           NOT NULL IDENTITY(1,1),
  FlightTelemetryId          UNIQUEIDENTIFIER NOT NULL,
  FlightTime                 INT                  NULL,
  Altitude                   INT                  NULL,
  VerticalSpeed              FLOAT                NULL,
  Heading                    FLOAT                NULL,
  Pitch                      FLOAT                NULL,
  Roll                       FLOAT                NULL,
  Yaw                        FLOAT                NULL,
  IsIncomplete               BIT                  NULL CONSTRAINT dfTelemetryFlightParameters_IsIncomplete DEFAULT(0),
  CONSTRAINT pkcTelemetryFlightParameters PRIMARY KEY (TelemetryFlightParameterId),
  CONSTRAINT fkTelemetryFlightParameters_Telemetry FOREIGN KEY (FlightTelemetryId) REFERENCES dbo.Telemetry (FlightTelemetryId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter',                                                            @value = N'Table to store the flight parameters data for a flight.',                                                                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'TelemetryFlightParameterId',               @value = N'Identifier of the flight parameter record.',                                                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'FlightTelemetryId',                        @value = N'Identifier of the Telemetry data for a flight.',                                                                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'FlightTime',                               @value = N'The time of the flight in milliseconds.',                                                                                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'Altitude',                                 @value = N'The altitude of the aircraft at the time of the Telemetry data.',                                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'VerticalSpeed',                            @value = N'The vertical speed of the aircraft at the time of the Telemetry data.',                                                   @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'Heading',                                  @value = N'The heading of the aircraft at the time of the Telemetry data.',                                                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'Pitch',                                    @value = N'The pitch of the aircraft at the time of the Telemetry data.',                                                            @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'Roll',                                     @value = N'The roll of the aircraft at the time of the Telemetry data.',                                                             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'Yaw',                                      @value = N'The yaw of the aircraft at the time of the Telemetry data.',                                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'IsIncomplete',                             @value = N'Indicates if the flight parameters data is incomplete.',                                                                  @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'pkcTelemetryFlightParameters',             @value = N'Defines the primary key for the TelemetryFlightParameters table using the TelemetryFlightParameterId column.',            @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'fkTelemetryFlightParameters_Telemetry',    @value = N'Defines the relationship between the TelemetryFlightParameters and Telemetry tables using the FlightTelemetryId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFlightParameter', @level2name = N'dfTelemetryFlightParameters_IsIncomplete', @value = N'Defines the default value for the IsIncomplete field as 0 (false).',                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO