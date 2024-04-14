namespace TaleLearnCode.FlightTracker.SubmitFlightPlan.Models;

public class SubmitFlightPlanDetails
{
	public required string FunctionHostAddress { get; set; }
	public string? OrchestratorInstanceId { get; set; }
	public required FlightPlanMessage FlightPlan { get; set; }
}