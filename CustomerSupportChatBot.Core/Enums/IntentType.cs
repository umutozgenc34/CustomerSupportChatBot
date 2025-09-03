namespace CustomerSupportChatBot.Core.Enums;

public enum IntentType
{
    Unknown = 0,
    Greeting = 1,
    Question = 2,
    Complaint = 3,
    Request = 4,
    Goodbye = 5,
    TechnicalSupport = 6,
    BillingInquiry = 7,
    ProductInfo = 8,
    OrderStatus = 9,
    Returns = 10
}