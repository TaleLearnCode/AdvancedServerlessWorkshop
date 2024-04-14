CREATE TABLE dbo.AircraftType
(
  AircraftTypeCode          VARCHAR(15) NOT NULL,
  AircraftTypeName          VARCHAR(50) NOT NULL,
  Manufacturer              VARCHAR(50) NOT NULL,
  TaxiSpeed                 INT         NOT NULL,
  RotationalSpeedLower      INT         NOT NULL,
  RotationalSpeedUpper      INT         NOT NULL,
  InitialClimbSpeedLower    INT         NOT NULL,
  InitialClimbSpeedUpper    INT         NOT NULL,
  ClimbSpeedLower           INT         NOT NULL,
  ClimbSpeedUpper           INT         NOT NULL,
  CruiseSpeedLower          INT         NOT NULL,
  CruiseSpeedUpper          INT         NOT NULL,
  DescentSpeedLower         INT         NOT NULL,
  DescentSpeedUpper         INT         NOT NULL,
  ApproachSpeedLower        INT         NOT NULL,
  ApproachSpeedUpper        INT         NOT NULL,
  LandingSpeedLower         INT         NOT NULL,
  LandingSpeedUpper         INT         NOT NULL,
  InitialClimbAltitudeLower INT         NOT NULL,
  InitialClimbAltitudeUpper INT         NOT NULL,
  CruiseAltitudeLower       INT         NOT NULL,
  CruiseAltitudeUpper       INT         NOT NULL,
  ApproachAltitudeLower     INT         NOT NULL,
  ApproachAltitudeUpper     INT         NOT NULL,
  CONSTRAINT pkcAircraftType PRIMARY KEY (AircraftTypeCode)
)
GO

EXEC sp_addextendedproperty @level1name = N'AircraftType',                                             @value = N'Lookup table of the different types of aircraft in use by the tracked flights.',                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE';
GO                                                                                                                                                                                                                           
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'AircraftTypeCode',          @value = N'The code for the aircraft type.',                                                                             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO                                                                                                                                                                                                                           
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'AircraftTypeName',          @value = N'The name of the aircraft type.',                                                                              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO                                                                                                                                                                                                                           
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'Manufacturer',              @value = N'The manufacturer of the aircraft type.',                                                                      @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'TaxiSpeed',                 @value = N'The taxi speed for the aircraft type in kilometers per hour (KPH).',                                          @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'RotationalSpeedLower',      @value = N'The lower bound of the rotational speed range for the aircraft type in kilometers per hour (KPH).',           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'RotationalSpeedUpper',      @value = N'The upper bound of the rotational speed range for the aircraft type in kilometers per hour (KPH).',           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'InitialClimbSpeedLower',    @value = N'The lower bound of the initial climb speed range for the aircraft type in kilometers per hour (KPH).',        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'InitialClimbSpeedUpper',    @value = N'The upper bound of the initial climb speed range for the aircraft type. in kilometers per hour (KPH)',        @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'ClimbSpeedLower',           @value = N'The lower bound of the climb speed range for the aircraft type in kilometers per hour (KPH).',                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'ClimbSpeedUpper',           @value = N'The upper bound of the climb speed range for the aircraft type in kilometers per hour (KPH).',                @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'CruiseSpeedLower',          @value = N'The lower bound of the cruise speed range for the aircraft type in kilometers per hour (KPH).',               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'CruiseSpeedUpper',          @value = N'The upper bound of the cruise speed range for the aircraft type in kilometers per hour (KPH).',               @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'DescentSpeedLower',         @value = N'The lower bound of the descent speed range for the aircraft type in kilometers per hour (KPH).',              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'DescentSpeedUpper',         @value = N'The upper bound of the descent speed range for the aircraft type in kilometers per hour (KPH).',              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'ApproachSpeedLower',        @value = N'The lower bound of the approach speed range for the aircraft type in kilometers per hour (KPH).',             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'ApproachSpeedUpper',        @value = N'The upper bound of the approach speed range for the aircraft type in kilometers per hour (KPH).',             @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'LandingSpeedLower',         @value = N'The lower bound of the landing speed range for the aircraft type in kilometers per hour (KPH).',              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'LandingSpeedUpper',         @value = N'The upper bound of the landing speed range for the aircraft type in kilometers per hour (KPH).',              @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'InitialClimbAltitudeLower', @value = N'The lower bound of the initial climb altitude range for the aircraft type in meters (m).',                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'InitialClimbAltitudeUpper', @value = N'The upper bound of the initial climb altitude range for the aircraft type in meters (m).',                    @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'CruiseAltitudeLower',       @value = N'The lower bound of the cruise altitude range for the aircraft type in meters (m).',                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'CruiseAltitudeUpper',       @value = N'The upper bound of the cruise altitude range for the aircraft type in meters (m).',                           @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'ApproachAltitudeLower',     @value = N'The lower bound of the approach altitude range for the aircraft type in meters (m).',                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'ApproachAltitudeUpper',     @value = N'The upper bound of the approach altitude range for the aircraft type in meters (m).',                         @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'COLUMN';
GO
EXEC sp_addextendedproperty @level1name = N'AircraftType', @level2name = N'pkcAircraftType',           @value = N'Defines the primary key for the AircraftType table using the AircraftTypeCode column.',                       @name = N'MS_Description', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level2type = N'CONSTRAINT';
GO