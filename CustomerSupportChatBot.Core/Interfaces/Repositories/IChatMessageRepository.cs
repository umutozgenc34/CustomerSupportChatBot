using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Enums;
using CustomerSupportChatBot.Core.Interfaces.Repositories.Base;

namespace CustomerSupportChatBot.Core.Interfaces.Repositories;

public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<IEnumerable<ChatMessage>> GetMessagesBySessionAsync(int sessionId);
    Task<IEnumerable<ChatMessage>> GetUnreadMessagesAsync(int sessionId);
    Task<IEnumerable<ChatMessage>> GetMessagesByTypeAsync(MessageType messageType);
    Task<ChatMessage?> GetLastMessageBySessionAsync(int sessionId);
    Task<IEnumerable<ChatMessage>> GetMessagesByIntentAsync(IntentType intentType);
    Task<int> GetUnreadMessageCountAsync(int sessionId);
    Task MarkMessagesAsReadAsync(int sessionId);
}