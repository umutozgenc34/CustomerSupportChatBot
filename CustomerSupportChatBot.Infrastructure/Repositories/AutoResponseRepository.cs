using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Enums;
using CustomerSupportChatBot.Core.Interfaces.Repositories;
using CustomerSupportChatBot.Infrastructure.Context;
using CustomerSupportChatBot.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Repositories;

public class AutoResponseRepository : Repository<AutoResponse>, IAutoResponseRepository
{
    public AutoResponseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AutoResponse>> GetActiveResponsesAsync()
    {
        return await _dbSet
            .Where(ar => ar.IsActive)
            .OrderBy(ar => ar.Order)
            .ThenByDescending(ar => ar.SuccessRate)
            .ToListAsync();
    }

    public async Task<IEnumerable<AutoResponse>> GetResponsesByIntentAsync(IntentType intentType)
    {
        return await _dbSet
            .Where(ar => ar.IsActive && ar.IntentType == intentType)
            .OrderBy(ar => ar.Order)
            .ThenByDescending(ar => ar.SuccessRate)
            .ToListAsync();
    }

    public async Task<AutoResponse?> FindBestMatchAsync(string message, IntentType? intent = null)
    {
        var query = _dbSet.Where(ar => ar.IsActive);

        if (intent.HasValue)
        {
            query = query.Where(ar => ar.IntentType == intent.Value);
        }

        var responses = await query.ToListAsync();

        var messageLower = message.ToLower();

        foreach (var response in responses.OrderBy(r => r.Order))
        {
            var keywords = response.Keywords.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (keywords.Any(keyword => messageLower.Contains(keyword.Trim())))
            {
                return response;
            }
        }

        return null;
    }

    public async Task<IEnumerable<AutoResponse>> GetResponsesByKeywordAsync(string keyword)
    {
        var keywordLower = keyword.ToLower();

        return await _dbSet
            .Where(ar => ar.IsActive && ar.Keywords.ToLower().Contains(keywordLower))
            .OrderBy(ar => ar.Order)
            .ToListAsync();
    }

    public async Task UpdateUsageCountAsync(int responseId)
    {
        var response = await _dbSet.FindAsync(responseId);
        if (response != null)
        {
            response.UsageCount++;
            _context.Update(response);
        }
    }
}
