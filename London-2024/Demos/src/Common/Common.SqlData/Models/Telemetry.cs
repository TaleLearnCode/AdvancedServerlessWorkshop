﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TaleLearnCode.FlightTrackingDemo.SqlData.Models;

/// <summary>
/// Table to store the Telemetry data for a flight.
/// </summary>
public partial class Telemetry
{
    /// <summary>
    /// Identifier of the Telemetry data for a flight.
    /// </summary>
    public Guid FlightTelemetryId { get; set; }

    /// <summary>
    /// Identifier of the Flight Plan for the Telemetry data.
    /// </summary>
    public Guid FlightPlanId { get; set; }

    /// <summary>
    /// Timestamp of the Telemetry data.
    /// </summary>
    public DateTime TelemetryTimestamp { get; set; }

    /// <summary>
    /// The phase of the flight at the time of the Telemetry data.
    /// </summary>
    public string FlightPhaseCode { get; set; }

    /// <summary>
    /// The sequential count of data recordings within the flight phase.
    /// </summary>
    public int? PhaseSequenceNubmer { get; set; }

    /// <summary>
    /// Indicates if the Telemetry data is incomplete.
    /// </summary>
    public bool? IsIncomplete { get; set; }

    public virtual FlightStatus FlightPhaseCodeNavigation { get; set; }

    public virtual FlightPlan FlightPlan { get; set; }

    public virtual ICollection<TelemetryElectricalSystem> TelemetryElectricalSystems { get; set; } = new List<TelemetryElectricalSystem>();

    public virtual ICollection<TelemetryEngineDatum> TelemetryEngineData { get; set; } = new List<TelemetryEngineDatum>();

    public virtual ICollection<TelemetryFlightParameter> TelemetryFlightParameters { get; set; } = new List<TelemetryFlightParameter>();

    public virtual ICollection<TelemetryFuelSystemDatum> TelemetryFuelSystemData { get; set; } = new List<TelemetryFuelSystemDatum>();

    public virtual ICollection<TelemetryHydraulicSystem> TelemetryHydraulicSystems { get; set; } = new List<TelemetryHydraulicSystem>();

    public virtual ICollection<TelemetryLocation> TelemetryLocations { get; set; } = new List<TelemetryLocation>();
}