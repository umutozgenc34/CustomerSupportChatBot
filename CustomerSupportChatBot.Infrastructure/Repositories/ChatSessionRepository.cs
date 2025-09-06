using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Enums;
using CustomerSupportChatBot.Core.Interfaces.Repositories;
using CustomerSupportChatBot.Infrastructure.Context;
using CustomerSupportChatBot.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Repositories;

public class ChatSessionRepository : Repository<ChatSession>, IChatSessionRepository
{
    public ChatSessionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ChatSession?> GetActiveSessionByCustomerIdAsync(int customerId)
    {
        return await _dbSet
            .Include(cs => cs.Customer)
            .Include(cs => cs.SupportAgent)
            .FirstOrDefaultAsync(cs => cs.CustomerId == customerId &&
                                     (cs.Status == SessionStatus.Active ||
                                      cs.Status == SessionStatus.WaitingForAgent ||
                                      cs.Status == SessionStatus.WithAgent));
    }

    public async Task<ChatSession?> GetBySessionTokenAsync(string sessionToken)
    {
        return await _dbSet
            .Include(cs => cs.Customer)
            .Include(cs => cs.SupportAgent)
            .FirstOrDefaultAsync(cs => cs.SessionToken == sessionToken);
    }

    public async Task<IEnumerable<ChatSession>> GetSessionsByStatusAsync(SessionStatus status)
    {
        return await _dbSet
            .Include(cs => cs.Customer)
            .Include(cs => cs.SupportAgent)
            .Where(cs => cs.Status == status)
            .OrderByDescending(cs => cs.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ChatSession>> GetSessionsByAgentAsync(int agentId)
    {
        return await _dbSet
            .Include(cs => cs.Customer)
            .Where(cs => cs.SupportAgentId == agentId)
            .OrderByDescending(cs => cs.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ChatSession>> GetActiveSessionsAsync()
    {
        return await _dbSet
            .Include(cs => cs.Customer)
            .Include(cs => cs.SupportAgent)
            .Where(cs => cs.Status == SessionStatus.Active ||
                        cs.Status == SessionStatus.WaitingForAgent ||
                        cs.Status == SessionStatus.WithAgent)
            .OrderByDescending(cs => cs.CreatedAt)
            .ToListAsync();
    }

    public async Task<ChatSession?> GetSessionWithMessagesAsync(int sessionId)
    {
        return await _dbSet
            .Include(cs => cs.Customer)
            .Include(cs => cs.SupportAgent)
            .Include(cs => cs.Messages.OrderBy(m => m.CreatedAt))
            .FirstOrDefaultAsync(cs => cs.Id == sessionId);
    }

    public async Task<IEnumerable<ChatSession>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(cs => cs.Customer)
            .Include(cs => cs.SupportAgent)
            .Where(cs => cs.CreatedAt >= startDate && cs.CreatedAt <= endDate)
            .OrderByDescending(cs => cs.CreatedAt)
            .ToListAsync();
    }
}