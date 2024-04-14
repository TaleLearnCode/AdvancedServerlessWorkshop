CREATE TABLE dbo.TelemetryEngineDataFaultCode
(
  TelemetryEngineDataFaultCodeId BIGINT           NOT NULL IDENTITY(1,1),
  TelemetryEngineDataId          BIGINT           NOT NULL,
  FaultCode                      VARCHAR(100)     NOT NULL,
  CONSTRAINT pkcTelemetryEngineDataFaultCode PRIMARY KEY (TelemetryEngineDataFaultCodeId),
  CONSTRAINT fkTelemetryEngineDataFaultCode_TelemetryEngineData FOREIGN KEY (TelemetryEngineDataId) REFERENCES dbo.TelemetryEngineData (TelemetryEngineDataId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryEngineDataFaultCode',                                                                      @value = N'Table to store the fault codes for an engine data record.',                                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineDataFaultCode', @level2name = N'TelemetryEngineDataFaultCodeId',                     @value = N'Identifier of the fault code for an engine data record.',                                                                                  @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineDataFaultCode', @level2name = N'TelemetryEngineDataId',                              @value = N'Identifier of the engine data for a flight.',                                                                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineDataFaultCode', @level2name = N'FaultCode',                                          @value = N'The fault code for an engine data record.',                                                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineDataFaultCode', @level2name = N'pkcTelemetryEngineDataFaultCode',                    @value = N'Defines the primary key for the TelemetryEngineDataFaultCode table using the TelemetryEngineDataFaultCodeId column.',                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryEngineDataFaultCode', @level2name = N'fkTelemetryEngineDataFaultCode_TelemetryEngineData', @value = N'Defines the relationship between the TelemetryEngineDataFaultCode and TelemetryEngineData tables using the TelemetryEngineDataId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO