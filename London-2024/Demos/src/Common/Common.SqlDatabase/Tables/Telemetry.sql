CREATE TABLE dbo.Telemetry
(
  FlightTelemetryId     UNIQUEIDENTIFIER NOT NULL,
  FlightPlanId          UNIQUEIDENTIFIER NOT NULL,
  TelemetryTimestamp    DATETIME         NOT NULL,
  FlightPhaseCode       CHAR(3)          NOT NULL,
  PhaseSequenceNubmer   INT                  NULL,
  IsIncomplete          BIT                  NULL CONSTRAINT dfTelemetry_IsIncomplete DEFAULT(0),
  CONSTRAINT pkcTelemetry PRIMARY KEY (FlightTelemetryId),
  CONSTRAINT fkTelemetry_FlightPlan FOREIGN KEY (FlightPlanId) REFERENCES dbo.FlightPlan (FlightPlanId),
  CONSTRAINT fkTelemetry_FlightPhase FOREIGN KEY (FlightPhaseCode) REFERENCES dbo.FlightStatus (FlightStatusCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'Telemetry',                                           @value = N'Table to store the Telemetry data for a flight.',                                                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'FlightTelemetryId',       @value = N'Identifier of the Telemetry data for a flight.',                                                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'FlightPlanId',            @value = N'Identifier of the Flight Plan for the Telemetry data.',                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'TelemetryTimestamp',      @value = N'Timestamp of the Telemetry data.',                                                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'FlightPhaseCode',         @value = N'The phase of the flight at the time of the Telemetry data.',                                               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'PhaseSequenceNubmer',     @value = N'The sequential count of data recordings within the flight phase.',                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'IsIncomplete',            @value = N'Indicates if the Telemetry data is incomplete.',                                                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'pkcTelemetry',            @value = N'Defines the primary key for the Telemetry table using the FlightTelemetryId column.',                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'fkTelemetry_FlightPlan',  @value = N'Defines the relationship between the Telemetry and FlightPlan tables using the FlightPlanId column.',      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'fkTelemetry_FlightPhase', @value = N'Defines the relationship between the Telemetry and FlightStatus tables using the FlightPhaseCode column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'Telemetry', @level2name = N'dfTelemetry_IsIncomplete', @value = N'Defines the default value for the IsIncomplete field as 0 (false).',                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO