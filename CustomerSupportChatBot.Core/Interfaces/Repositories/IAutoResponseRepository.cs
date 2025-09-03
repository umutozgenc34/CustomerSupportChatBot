using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Enums;
using CustomerSupportChatBot.Core.Interfaces.Repositories.Base;

namespace CustomerSupportChatBot.Core.Interfaces.Repositories;

public interface IAutoResponseRepository : IRepository<AutoResponse>
{
    Task<IEnumerable<AutoResponse>> GetActiveResponsesAsync();
    Task<IEnumerable<AutoResponse>> GetResponsesByIntentAsync(IntentType intentType);
    Task<AutoResponse?> FindBestMatchAsync(string message, IntentType? intent = null);
    Task<IEnumerable<AutoResponse>> GetResponsesByKeywordAsync(string keyword);
    Task UpdateUsageCountAsync(int responseId);
}