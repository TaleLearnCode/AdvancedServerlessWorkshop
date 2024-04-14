namespace TaleLearnCode.FlightTracker.SubmitFlightPlan.Models;

public record SendEmailRequest(string RecipientAddress, string Subject, string HtmlContent, string PlainTextContent);