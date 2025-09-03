namespace CustomerSupportChatBot.Core.Enums;

public enum SessionStatus
{
    Active = 1,
    WaitingForAgent = 2,
    WithAgent = 3,
    Closed = 4,
    Timeout = 5
}
