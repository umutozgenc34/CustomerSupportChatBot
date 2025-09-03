using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Enums;
using CustomerSupportChatBot.Core.Interfaces.Repositories.Base;

namespace CustomerSupportChatBot.Core.Interfaces.Repositories;

public interface IChatSessionRepository : IRepository<ChatSession>
{
    Task<ChatSession?> GetActiveSessionByCustomerIdAsync(int customerId);
    Task<ChatSession?> GetBySessionTokenAsync(string sessionToken);
    Task<IEnumerable<ChatSession>> GetSessionsByStatusAsync(SessionStatus status);
    Task<IEnumerable<ChatSession>> GetSessionsByAgentAsync(int agentId);
    Task<IEnumerable<ChatSession>> GetActiveSessionsAsync();
    Task<ChatSession?> GetSessionWithMessagesAsync(int sessionId);
    Task<IEnumerable<ChatSession>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate);
}