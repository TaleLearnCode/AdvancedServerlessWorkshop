CREATE TABLE dbo.TelemetryFuelSystemData
(
  TelemetryFuelSystemDataId BIGINT           NOT NULL IDENTITY(1,1),
  FlightTelemetryId         UNIQUEIDENTIFIER NOT NULL,
  FuelQuantity              FLOAT                NULL,
  FuelConsumptionRate       FLOAT                NULL,
  IsIncomplete              BIT                  NULL CONSTRAINT dfTelemetryFuelSystemData_IsIncomplete DEFAULT(0),
  CONSTRAINT pkcTelemetryFuelSystemData PRIMARY KEY (TelemetryFuelSystemDataId),
  CONSTRAINT fkTelemetryFuelSystemData_Telemetry FOREIGN KEY (FlightTelemetryId) REFERENCES dbo.Telemetry (FlightTelemetryId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData',                                                          @value = N'Table to store the fuel system data for a flight.',                                                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData', @level2name = N'TelemetryFuelSystemDataId',              @value = N'Identifier of the fuel system data for a flight.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData', @level2name = N'FlightTelemetryId',                      @value = N'Identifier of the Telemetry data for a flight.',                                                                        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData', @level2name = N'FuelQuantity',                           @value = N'The quantity of fuel in the fuel system in liters.',                                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData', @level2name = N'FuelConsumptionRate',                    @value = N'The rate of fuel consumption in liters per hour.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData', @level2name = N'IsIncomplete',                           @value = N'Indicates if the fuel system data is incomplete.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData', @level2name = N'pkcTelemetryFuelSystemData',             @value = N'Defines the primary key for the TelemetryFuelSystemData table using the TelemetryFuelSystemDataId column.',             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData', @level2name = N'fkTelemetryFuelSystemData_Telemetry',    @value = N'Defines the relationship between the TelemetryFuelSystemData and Telemetry tables using the FlightTelemetryId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryFuelSystemData', @level2name = N'dfTelemetryFuelSystemData_IsIncomplete', @value = N'Defines the default value for the IsIncomplete field as 0 (false).',                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO