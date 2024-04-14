namespace Drone.Common
{
	public interface IEventHubHelper
	{
		Task SendMessageAsync(string message);
	}
}