CREATE TABLE dbo.FlightPlan
(
  FlightPlanId           UNIQUEIDENTIFIER NOT NULL,
  AirlineCode            CHAR(2)          NOT NULL,
  FlightNumber           CHAR(4)          NOT NULL,
  OriginAirportCode      CHAR(3)          NOT NULL,
  DestinationAirportCode CHAR(3)          NOT NULL,
  AircraftTypeCode       VARCHAR(15)      NOT NULL,
  DepartureTime          DATETIME         NOT NULL,
  ArrivalTime            DATETIME         NOT NULL,
  Bearing                FLOAT            NOT NULL,
  RotationalSpeed        INT              NOT NULL,
  InitialClimbAltitude   INT              NOT NULL,
  CruiseAltitude         INT              NOT NULL,
  StartDescentDistance   INT              NOT NULL,
  StartApproachAltitude  INT              NOT NULL,
  CurrentFlightPhase     CHAR(3)          NOT NULL,
  LandingSpeed           INT              NOT NULL,
  CONSTRAINT pkcFlightPlan PRIMARY KEY (FlightPlanId),
  CONSTRAINT fkFlightPlan_Airline FOREIGN KEY (AirlineCode) REFERENCES dbo.Airline (IATACode),
  CONSTRAINT fkFlightPlan_Airport_Origin FOREIGN KEY (OriginAirportCode) REFERENCES dbo.Airport (IATACode),
  CONSTRAINT fkFlightPlan_Airport_Destination FOREIGN KEY (DestinationAirportCode) REFERENCES dbo.Airport (IATACode),
  CONSTRAINT fkFlightPlan_AircraftType FOREIGN KEY (AircraftTypeCode) REFERENCES dbo.AircraftType (AircraftTypeCode),
  CONSTRAINT fkFlightPlan_FlightStatus FOREIGN KEY (CurrentFlightPhase) REFERENCES dbo.FlightStatus (FlightStatusCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'FlightPlan',                                                    @value = N'Filed plans of flights to be tracked by the system.',                                                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'FlightPlanId',                     @value = N'Identifier of the flight plan.',                                                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'AirlineCode',                      @value = N'The IATA code of the airline.',                                                                               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'FlightNumber',                     @value = N'The flight number assigned by the airline.',                                                                  @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'OriginAirportCode',                @value = N'The IATA code of the airport where the flight departs.',                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'DestinationAirportCode',           @value = N'The IATA code of the airport where the flight arrives.',                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'AircraftTypeCode',                 @value = N'The type of aircraft used for the flight.',                                                                   @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'DepartureTime',                    @value = N'The date and time when the flight departs.',                                                                  @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'ArrivalTime',                      @value = N'The date and time when the flight arrives.',                                                                  @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'Bearing',                          @value = N'The direction of the flight in degrees.',                                                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'RotationalSpeed',                  @value = N'The speed at which the aircraft rotates during takeoff.',                                                     @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'InitialClimbAltitude',             @value = N'The altitude at which the aircraft starts climbing after takeoff.',                                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'CruiseAltitude',                   @value = N'The altitude at which the aircraft cruises during the flight.',                                               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'StartDescentDistance',             @value = N'The distance from the destination airport at which the aircraft starts descending.',                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'StartApproachAltitude',            @value = N'The altitude at which the aircraft starts the approach to the destination airport.',                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'CurrentFlightPhase',               @value = N'The current phase of the flight.',                                                                            @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'LandingSpeed',                     @value = N'The speed at which the aircraft lands.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'pkcFlightPlan',                    @value = N'Defines the primary key for the FlightPlan table using the FlightPlanId column.',                             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'fkFlightPlan_Airline',             @value = N'Defines the relationship between the FlightPlan and Airline tables using the AirlineCode column.',            @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'fkFlightPlan_Airport_Origin',      @value = N'Defines the relationship between the FlightPlan and Airport tables using the OriginAirportCode column.',      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'fkFlightPlan_Airport_Destination', @value = N'Defines the relationship between the FlightPlan and Airport tables using the DestinationAirportCode column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'fkFlightPlan_AircraftType',        @value = N'Defines the relationship between the FlightPlan and AircraftType tables using the AircraftType column.',      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level1name = N'FlightPlan', @level2name = N'fkFlightPlan_FlightStatus',        @value = N'Defines the relationship between the FlightPlan and FlightStatus tables using the CurrentFlightPhase column.', @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO