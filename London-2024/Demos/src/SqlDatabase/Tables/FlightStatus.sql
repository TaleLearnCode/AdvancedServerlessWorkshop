CREATE TABLE dbo.FlightStatus
(
  FlightStatusCode        CHAR(3)      NOT NULL,
  FlightStatusName        VARCHAR(50)  NOT NULL,
  FlightStatusDescription VARCHAR(255) NOT NULL,
  CONSTRAINT pkcFlightStatus PRIMARY KEY (FlightStatusCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'FlightStatus',                                             @value = N'Lookup table of the different status of a flight.',                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO                                                                                                                                                                                                                           
EXEC sp_addextendedproperty @level1name = N'FlightStatus', @level2name = N'FlightStatusCode',          @value = N'The code for the flight status.',                                                       @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO                                                                                                                                                                                                                           
EXEC sp_addextendedproperty @level1name = N'FlightStatus', @level2name = 'FlightStatusName',           @value = N'The name of the flight status.',                                                        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightStatus', @level2name = 'FlightStatusDescription',    @value = N'The description of the flight status.',                                                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightStatus', @level2name = N'pkcFlightStatus',           @value = N'Defines the primary key for the FlightStatus table using the FlightStatusCode column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO