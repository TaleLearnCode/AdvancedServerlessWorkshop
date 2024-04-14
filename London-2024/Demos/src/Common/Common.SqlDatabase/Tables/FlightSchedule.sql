CREATE TABLE dbo.FlightSchedule
(
  FlightScheduleCode   CHAR(7)     NOT NULL,
  AirlineCode          CHAR(2)     NOT NULL,
  FlightNumber         CHAR(4)     NOT NULL,
  DepartureAirportCode CHAR(3)     NOT NULL,
  ArrivalAirportCode   CHAR(3)     NOT NULL,
  DepartureTime        TIME        NOT NULL,
  ArrivalTime          TIME        NOT NULL,
  FlightTime           INT         NOT NULL,
  AircraftTypeCode     VARCHAR(15) NOT NULL,
  CONSTRAINT pkcFlightSchedule PRIMARY KEY (FlightScheduleCode),
  CONSTRAINT fkFlightSchedule_Airline           FOREIGN KEY (AirlineCode)          REFERENCES dbo.Airline (IATACode),
  CONSTRAINT fkFlightSchedule_Airport_Departure FOREIGN KEY (DepartureAirportCode) REFERENCES dbo.Airport (IATACode),
  CONSTRAINT fkFlightSchedule_Airport_Arrival   FOREIGN KEY (ArrivalAirportCode)   REFERENCES dbo.Airport (IATACode),
  CONSTRAINT fkFlightSchedule_AircraftType      FOREIGN KEY (AircraftTypeCode)     REFERENCES dbo.AircraftType (AircraftTypeCode),
  CONSTRAINT uqcFlightSchedule UNIQUE (AirlineCode, FlightNumber)
)
GO

EXEC sp_addextendedproperty @level1name = N'FlightSchedule',                                                       @value = N'Lookup table representing the daily flight schedule.',                                                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'FlightScheduleCode',                  @value = N'Identifier of the scheduled daily flight.',                                                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'AirlineCode',                         @value = N'The IATA code of the airline operating the flight.',                                                            @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'FlightNumber',                        @value = N'The flight number for the scheduled flight.',                                                                   @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'DepartureAirportCode',                @value = N'The IATA code of the airport where the flight departs.',                                                        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'ArrivalAirportCode',                  @value = N'The IATA code of the airport where the flight arrives.',                                                        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'DepartureTime',                       @value = N'The time of day when the flight departs.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'ArrivalTime',                         @value = N'The time of day when the flight arrives.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'FlightTime',                          @value = N'The duration of the flight in minutes.',                                                                        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'AircraftTypeCode',                    @value = N'The type of aircraft used for the flight.',                                                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'pkcFlightSchedule',                  @value = N'Defines the primary key for the FlightSchedule table using the FlightScheduleId column.',                       @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'fkFlightSchedule_Airline',           @value = N'Defines the relationship between the FlightSchedule and Airline tables using the AirlineCode column.',          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'fkFlightSchedule_Airport_Departure', @value = N'Defines the relationship between the FlightSchedule and Airport tables using the DepartureAirportCode column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'fkFlightSchedule_Airport_Arrival',   @value = N'Defines the relationship between the FlightSchedule and Airport tables using the ArrivalAirportCode column.',   @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'fkFlightSchedule_AircraftType',      @value = N'Defines the relationship between the FlightSchedule and AircraftType tables using the AircraftType column.',    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightSchedule', @level2name = N'uqcFlightSchedule',                  @value = N'Uniqueness constraint to ensure that the combination of AirlineCode and FlightNumber is unique.',               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO