CREATE TABLE dbo.TelemetryHydraulicSystem
(
  TelemetryHydraulicSystemId BIGINT           NOT NULL IDENTITY(1,1),
  FlightTelemetryId          UNIQUEIDENTIFIER NOT NULL,
  HydraulicPressure          FLOAT                NULL,
  ActuatorStatus             VARCHAR(100)         NULL,
  IsIncomplete               BIT                  NULL CONSTRAINT dfTelemetryHydraulicSystem_IsIncomplete DEFAULT(0),
  CONSTRAINT pkcTelemetryHydraulicSystem PRIMARY KEY (TelemetryHydraulicSystemId),
  CONSTRAINT fkTelemetryHydraulicSystem_Telemetry FOREIGN KEY (FlightTelemetryId) REFERENCES dbo.Telemetry (FlightTelemetryId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem',                                                           @value = N'Table to store the hydraulic system data for a flight.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem', @level2name = N'TelemetryHydraulicSystemId',              @value = N'Identifier of the hydraulic system data for a flight.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem', @level2name = N'FlightTelemetryId',                       @value = N'Identifier of the Telemetry data for a flight.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem', @level2name = N'HydraulicPressure',                       @value = N'The pressure of the hydraulic system in psi.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem', @level2name = N'ActuatorStatus',                          @value = N'The status of the actuators in the hydraulic system.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem', @level2name = N'IsIncomplete',                            @value = N'Indicates if the hydraulic system data is incomplete.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem', @level2name = N'pkcTelemetryHydraulicSystem',             @value = N'Defines the primary key for the TelemetryHydraulicSystem table using the TelemetryHydraulicSystemId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem', @level2name = N'fkTelemetryHydraulicSystem_Telemetry',    @value = N'Defines the relationship between the TelemetryHydraulicSystem and Telemetry tables using the FlightTelemetryId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystem', @level2name = N'dfTelemetryHydraulicSystem_IsIncomplete', @value = N'Defines the default value for the IsIncomplete field as 0 (false).', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO