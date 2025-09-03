namespace CustomerSupportChatBot.Core.Exceptions;

public class AgentNotAvailableException : ChatException
{
    public AgentNotAvailableException()
        : base("No support agent is currently available.", "AGENT_NOT_AVAILABLE")
    {
    }
    public AgentNotAvailableException(string department)
        : base($"No support agent is available in {department} department.", "AGENT_NOT_AVAILABLE")
    {
    }
}