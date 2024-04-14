CREATE TABLE dbo.TelemetryHydraulicSystemFluidLevel
(
  TeleHydraulicSystemFluidLevelId BIGINT           NOT NULL IDENTITY(1,1),
  TelemetryHydraulicSystemId      BIGINT           NOT NULL,
  FluidLevel                      FLOAT            NOT NULL,
  CONSTRAINT pkcTelemetryHydraulicSystemFluidLevel PRIMARY KEY (TeleHydraulicSystemFluidLevelId),
  CONSTRAINT fkTelemetryHydraulicSystemFluidLevel_TelemetryHydraulicSystem FOREIGN KEY (TelemetryHydraulicSystemId) REFERENCES dbo.TelemetryHydraulicSystem (TelemetryHydraulicSystemId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystemFluidLevel',                                                                                 @value = N'Table to store the fluid levels for a hydraulic system data record.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystemFluidLevel', @level2name = N'TeleHydraulicSystemFluidLevelId',                               @value = N'Identifier of the fluid level for a hydraulic system data record.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystemFluidLevel', @level2name = N'TelemetryHydraulicSystemId',                                    @value = N'Identifier of the hydraulic system data for a flight.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystemFluidLevel', @level2name = N'FluidLevel',                                                    @value = N'The fluid level in the hydraulic system in liters.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystemFluidLevel', @level2name = N'pkcTelemetryHydraulicSystemFluidLevel',                         @value = N'Defines the primary key for the TelemetryHydraulicSystemFluidLevel table using the TeleHydraulicSystemFluidLevelId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryHydraulicSystemFluidLevel', @level2name = N'fkTelemetryHydraulicSystemFluidLevel_TelemetryHydraulicSystem', @value = N'Defines the relationship between the TelemetryHydraulicSystemFluidLevel and TelemetryHydraulicSystem tables using the TelemetryHydraulicSystemId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO