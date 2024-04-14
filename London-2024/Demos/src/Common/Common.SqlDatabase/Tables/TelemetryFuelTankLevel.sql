CREATE TABLE dbo.TelemetryFuelTankLevel
(
  TelemetryFuelTankLevelId  BIGINT NOT NULL IDENTITY(1,1),
  TelemetryFuelSystemDataId BIGINT NOT NULL,
  TankNumber                INT    NOT NULL,
  FuelLevel                 FLOAT  NOT NULL,
  CONSTRAINT pkcTelemetryFuelTankLevel PRIMARY KEY (TelemetryFuelTankLevelId),
  CONSTRAINT fkTelemetryFuelTankLevel_TelemetryFuelSystemData FOREIGN KEY (TelemetryFuelSystemDataId) REFERENCES dbo.TelemetryFuelSystemData (TelemetryFuelSystemDataId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryFuelTankLevel',                                                                    @value = N'Table to store the fuel tank levels for a fuel system data record.',                                                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelTankLevel', @level2name = N'TelemetryFuelTankLevelId',                         @value = N'Identifier of the fuel tank level for a fuel system data record.',                                                                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelTankLevel', @level2name = N'TelemetryFuelSystemDataId',                        @value = N'Identifier of the fuel system data for a flight.',                                                                                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelTankLevel', @level2name = N'TankNumber',                                       @value = N'The number of the fuel tank.',                                                                                                               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelTankLevel', @level2name = N'FuelLevel',                                        @value = N'The level of fuel in the fuel tank in liters.',                                                                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelTankLevel', @level2name = N'pkcTelemetryFuelTankLevel',                        @value = N'Defines the primary key for the TelemetryFuelTankLevel table using the TelemetryFuelTankLevelId column.',                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelTankLevel', @level2name = N'fkTelemetryFuelTankLevel_TelemetryFuelSystemData', @value = N'Defines the relationship between the TelemetryFuelTankLevel and TelemetryFuelSystemData tables using the TelemetryFuelSystemDataId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO