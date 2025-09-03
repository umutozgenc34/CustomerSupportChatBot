namespace CustomerSupportChatBot.Core.Exceptions;

public class NLPException : Exception
{
    public string ErrorCode { get; }
    public NLPException(string message) : base(message)
    {
        ErrorCode = "NLP_ERROR";
    }

    public NLPException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public NLPException(string message, Exception innerException) : base(message, innerException)
    {
        ErrorCode = "NLP_ERROR";
    }
}