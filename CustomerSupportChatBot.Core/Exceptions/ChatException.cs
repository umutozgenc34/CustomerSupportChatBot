namespace CustomerSupportChatBot.Core.Exceptions;

public class ChatException : Exception
{
    public string ErrorCode { get; }

    public ChatException(string message) : base(message)
    {
        ErrorCode = "CHAT_ERROR";
    }
    public ChatException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }
    public ChatException(string message, Exception innerException) : base(message, innerException)
    {
        ErrorCode = "CHAT_ERROR";
    }
    public ChatException(string message, string errorCode, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}