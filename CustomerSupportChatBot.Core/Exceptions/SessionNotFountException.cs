namespace CustomerSupportChatBot.Core.Exceptions;

public class SessionNotFoundException : ChatException
{
    public SessionNotFoundException(int sessionId)
        : base($"Chat session with ID {sessionId} was not found.", "SESSION_NOT_FOUND")
    {
    }
    public SessionNotFoundException(string sessionToken)
        : base($"Chat session with token {sessionToken} was not found.", "SESSION_NOT_FOUND")
    {
    }
}