CREATE TABLE dbo.Airport
(
  IATACode    CHAR(3)       NOT NULL,
  ICAOCode    CHAR(4)       NOT NULL,
  AirportName VARCHAR(50)   NOT NULL,
  CityName    VARCHAR(50)   NOT NULL,
  CountryCode CHAR(2)       NOT NULL,
  Elevation   INT           NOT NULL,
  Latitude    DECIMAL(9, 6) NOT NULL,
  Longitude   DECIMAL(9, 6) NOT NULL,
  IsActive    BIT           NOT NULL,
  CONSTRAINT pkcAirport PRIMARY KEY (IATACode),
  CONSTRAINT fkcAirportCountry FOREIGN KEY (CountryCode) REFERENCES dbo.Country (CountryCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'Airport',                                     @value = N'Lookup table representing the aiports supported by the system.',                               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'IATACode',          @value = N'The IATA code for the airline.',                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'ICAOCode',          @value = N'The ICAO code for the airline.',                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'AirportName',       @value = N'The name of the airport.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'CityName',          @value = N'The name of the city where the airport is located.',                                             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'CountryCode',       @value = N'The code of the country where the airport is located.',                                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'Elevation',         @value = N'The elevation of the airport in feet.',                                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'Latitude',          @value = N'The latitude of the airport.',                                                                 @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'Longitude',         @value = N'The longitude of the airport.',                                                                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'IsActive',          @value = N'Flag indicating whether the airport is active within the system.',                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'pkcAirport',        @value = N'Defines the primary key for the Airport table using the IATACode column.',                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'Airport', @level2name = N'fkcAirportCountry', @value = N'Defines the relationship between the Airport and Country tables using the CountryCode column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO