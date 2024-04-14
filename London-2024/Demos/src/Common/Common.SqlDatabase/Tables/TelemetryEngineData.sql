CREATE TABLE dbo.TelemetryEngineData
(
  TelemetryEngineDataId BIGINT           NOT NULL IDENTITY(1,1),
  FlightTelemetryId     UNIQUEIDENTIFIER NOT NULL,
  EngineNumber          INT              NOT NULL,
  EngineSpeed           FLOAT                NULL,
  FuelFlowRate          FLOAT                NULL,
  OilPressure           FLOAT                NULL,
  OilTemperature        FLOAT                NULL,
  ExhaustGasTemperature FLOAT                NULL,
  Thrust                FLOAT                NULL,
  IsIncomplete          BIT                  NULL CONSTRAINT dfTelemetryEngineData_IsIncomplete DEFAULT(0),
  CONSTRAINT pkcTelemetryEngineData PRIMARY KEY (TelemetryEngineDataId),
  CONSTRAINT fkTelemetryEngineData_Telemetry FOREIGN KEY (FlightTelemetryId) REFERENCES dbo.Telemetry (FlightTelemetryId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData',                                                   @value = N'Table to store the engine data for a flight.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'TelemetryEngineDataId',           @value = N'Identifier of the engine data for a flight.',                                                                       @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'FlightTelemetryId',               @value = N'Identifier of the Telemetry data for a flight.',                                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'EngineNumber',                    @value = N'The number of the engine.',                                                                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'EngineSpeed',                     @value = N'The speed of the engine in revolutions per minute.',                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'FuelFlowRate',                    @value = N'The rate of fuel flow to the engine in liters per hour.',                                                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'OilPressure',                     @value = N'The pressure of the engine oil in kilopascals.',                                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'OilTemperature',                  @value = N'The temperature of the engine oil in degrees Celsius.',                                                             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'ExhaustGasTemperature',           @value = N'The temperature of the engine exhaust gases in degrees Celsius.',                                                   @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'Thrust',                          @value = N'The thrust of the engine in newtons.',                                                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'IsIncomplete',                    @value = N'Indicates if the engine data is incomplete.',                                                                       @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'pkcTelemetryEngineData',          @value = N'Defines the primary key for the TelemetryEngineData table using the TelemetryEngineDataId column.',                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'fkTelemetryEngineData_Telemetry', @value = N'Defines the relationship between the TelemetryEngineData and Telemetry tables using the FlightTelemetryId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineData', @level2name = N'dfTelemetryEngineData_IsIncomplete', @value = N'Defines the default value for the IsIncomplete field as 0 (false).', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO