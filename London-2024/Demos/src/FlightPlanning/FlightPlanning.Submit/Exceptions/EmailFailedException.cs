namespace TaleLearnCode.FlightTracker.SubmitFlightPlan.Exceptions;

public class EmailFailedException : Exception
{
	public EmailFailedException() { }
	public EmailFailedException(string message) : base(message) { }
	public EmailFailedException(string message, Exception innerException) : base(message, innerException) { }
}