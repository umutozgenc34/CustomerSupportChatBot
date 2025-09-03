using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Interfaces.Repositories.Base;

namespace CustomerSupportChatBot.Core.Interfaces.Repositories;

public interface ISupportAgentRepository : IRepository<SupportAgent>
{
    Task<IEnumerable<SupportAgent>> GetOnlineAgentsAsync();
    Task<IEnumerable<SupportAgent>> GetAvailableAgentsAsync();
    Task<SupportAgent?> GetByEmailAsync(string email);
    Task<SupportAgent?> GetLeastBusyAgentAsync();
    Task UpdateAgentStatusAsync(int agentId, bool isOnline, bool isAvailable);
    Task IncrementActiveChatCountAsync(int agentId);
    Task DecrementActiveChatCountAsync(int agentId);
}