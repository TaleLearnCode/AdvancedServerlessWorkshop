CREATE TABLE dbo.Airline
(
  IATACode    CHAR(2)     NOT NULL,
  ICAOCode    CHAR(3)     NOT NULL,
  AirlineName VARCHAR(50) NOT NULL,
  Callsign    VARCHAR(50) NOT NULL,
  CountryCode CHAR(2)     NOT NULL,
  IsActive    BIT         NOT NULL,
  CONSTRAINT pkcAirline PRIMARY KEY (IATACode),
  CONSTRAINT fkcAirlineCountry FOREIGN KEY (CountryCode) REFERENCES dbo.Country (CountryCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'Airline',                                     @value = N'Lookup table representing the airlines supported by the system.',                               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'Airline', @level2name = N'IATACode',          @value = N'The IATA code for the airline.',                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airline', @level2name = N'ICAOCode',          @value = N'The ICAO code for the airline.',                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airline', @level2name = N'AirlineName',       @value = N'The name of the airline.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airline', @level2name = N'Callsign',          @value = N'The callsign for the airline.',                                                                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airline', @level2name = N'CountryCode',       @value = N'The code of the country where the airline is headquatered.',                                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airline', @level2name = N'IsActive',          @value = N'Flag indicating whether the airline is active within the system.',                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airline', @level2name = N'pkcAirline',        @value = N'Defines the primary key for the Airline table using the IATACode column.',                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'Airline', @level2name = N'fkcAirlineCountry', @value = N'Defines the relationship between the Airline and Country tables using the CountryCode column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO