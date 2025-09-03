namespace CustomerSupportChatBot.Core.Exceptions;

public class SessionClosedException : ChatException
{
    public SessionClosedException(int sessionId)
        : base($"Chat session with ID {sessionId} is already closed.", "SESSION_CLOSED")
    {
    }
}
