CREATE TABLE dbo.CustomerFlightStatus
(
  CustomerFlightStatusCode        CHAR(3)      NOT NULL,
  CustomerFlightStatusName        VARCHAR(50)  NOT NULL,
  CustomerFlightStatusDescription VARCHAR(255) NOT NULL,
  AssociatedFlightStatusCode      CHAR(3)      NOT NULL,
  CONSTRAINT pkcCustomerFlightStatus PRIMARY KEY (CustomerFlightStatusCode),
  CONSTRAINT fkCustomerFlightStatus_FlightStatus FOREIGN KEY (AssociatedFlightStatusCode) REFERENCES dbo.FlightStatus (FlightStatusCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatus',                                                       @value = N'Lookup table of the different status of a customer''s flight.',                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatus', @level2name = 'CustomerFlightStatusCode',             @value = N'The code for the customer''s flight status.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatus', @level2name = 'CustomerFlightStatusName',             @value = N'The name of the customer''s flight status.',                                                                       @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatus', @level2name = 'CustomerFlightStatusDescription',      @value = N'The description of the customer''s flight status.',                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatus', @level2name = 'AssociatedFlightStatusCode',           @value = N'The code of the associated flight status.',                                                                        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatus', @level2name = 'pkcCustomerFlightStatus',              @value = N'Defines the primary key for the CustomerFlightStatus table using the CustomerFlightStatusCode column.',            @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatus', @level2name = N'fkCustomerFlightStatus_FlightStatus', @value = N'Defines the relationship between the CustomerFlightStatus and AircraftType tables using the FlightStatus column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO