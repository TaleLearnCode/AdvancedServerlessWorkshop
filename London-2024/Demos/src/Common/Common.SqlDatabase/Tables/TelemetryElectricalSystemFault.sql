CREATE TABLE dbo.TelemetryElectricalSystemFault
(
  TelemetryElectricalSystemFaultId BIGINT           NOT NULL IDENTITY(1,1),
  TelemetryElectricalSystemId      BIGINT           NOT NULL,
  FaultCode                        VARCHAR(100)     NOT NULL,
  CONSTRAINT pkcTelemetryElectricalSystemFault PRIMARY KEY (TelemetryElectricalSystemFaultId),
  CONSTRAINT fkTelemetryElectricalSystemFault_TelemetryElectricalSystem FOREIGN KEY (TelemetryElectricalSystemId) REFERENCES dbo.TelemetryElectricalSystem (TeleElectricalSystemId)
)
GO

EXEC sp_addextendedproperty @level1name = N'TelemetryElectricalSystemFault', @value = N'Table to store the fault codes for an electrical system data record.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryElectricalSystemFault', @level2name = N'TelemetryElectricalSystemFaultId', @value = N'Identifier of the fault code for an electrical system data record.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryElectricalSystemFault', @level2name = N'TelemetryElectricalSystemId', @value = N'Identifier of the electrical system data for a flight.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryElectricalSystemFault', @level2name = N'FaultCode', @value = N'The fault code for an electrical system data record.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryElectricalSystemFault', @level2name = N'pkcTelemetryElectricalSystemFault', @value = N'Defines the primary key for the TelemetryElectricalSystemFault table using the TelemetryElectricalSystemFaultId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'TelemetryElectricalSystemFault', @level2name = N'fkTelemetryElectricalSystemFault_TelemetryElectricalSystem', @value = N'Defines the relationship between the TelemetryElectricalSystemFault and TelemetryElectricalSystem tables using the TelemetryElectricalSystemId column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO