CREATE TABLE dbo.CustomerFlightStatusToFlightStatus
(
  CustomerFlightStatusToFlightStatusId INT     NOT NULL,
  CustomerFlightStatusCode             CHAR(3) NOT NULL,
  FlightStatusCode                     CHAR(3) NOT NULL,
  CONSTRAINT pkcCustomerFlightStatusToFlightStatus PRIMARY KEY (CustomerFlightStatusToFlightStatusId),
  CONSTRAINT fkCustomerFlightStatusToFlightStatus_CustomerFlightStatus FOREIGN KEY (CustomerFlightStatusCode) REFERENCES dbo.CustomerFlightStatus (CustomerFlightStatusCode),
  CONSTRAINT fkCustomerFlightStatusToFlightStatus_FlightStatus FOREIGN KEY (FlightStatusCode) REFERENCES dbo.FlightStatus (FlightStatusCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatusToFlightStatus',                                                                            @value = N'Lookup table to associate a customer''s flight status with a flight status.',                                                                        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatusToFlightStatus', @level2name = 'CustomerFlightStatusToFlightStatusId',                      @value = N'The unique identifier for the CustomerFlightStatusToFlightStatus table.',                                                                            @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatusToFlightStatus', @level2name = 'CustomerFlightStatusCode',                                  @value = N'The code for the customer''s flight status.',                                                                                                        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatusToFlightStatus', @level2name = 'FlightStatusCode',                                          @value = N'The code for the flight status.',                                                                                                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatusToFlightStatus', @level2name = 'pkcCustomerFlightStatusToFlightStatus',                     @value = N'Defines the primary key for the CustomerFlightStatusToFlightStatus table using the CustomerFlightStatusToFlightStatusId column.',                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatusToFlightStatus', @level2name = 'fkCustomerFlightStatusToFlightStatus_CustomerFlightStatus', @value = N'Defines the relationship between the CustomerFlightStatusToFlightStatus and CustomerFlightStatus tables using the CustomerFlightStatusCode column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'CustomerFlightStatusToFlightStatus', @level2name = 'fkCustomerFlightStatusToFlightStatus_FlightStatus',         @value = N'Defines the relationship between the CustomerFlightStatusToFlightStatus and FlightStatus tables using the FlightStatusCode column.',                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO