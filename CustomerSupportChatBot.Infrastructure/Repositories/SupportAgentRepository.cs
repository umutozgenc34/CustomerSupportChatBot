using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Interfaces.Repositories;
using CustomerSupportChatBot.Infrastructure.Context;
using CustomerSupportChatBot.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Repositories;

public class SupportAgentRepository : Repository<SupportAgent>, ISupportAgentRepository
{
    public SupportAgentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SupportAgent>> GetOnlineAgentsAsync()
    {
        return await _dbSet
            .Where(sa => sa.IsOnline)
            .OrderBy(sa => sa.CurrentActiveChats)
            .ToListAsync();
    }

    public async Task<IEnumerable<SupportAgent>> GetAvailableAgentsAsync()
    {
        return await _dbSet
            .Where(sa => sa.IsOnline && sa.IsAvailable &&
                        sa.CurrentActiveChats < sa.MaxConcurrentChats)
            .OrderBy(sa => sa.CurrentActiveChats)
            .ToListAsync();
    }

    public async Task<SupportAgent?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(sa => sa.Email == email);
    }

    public async Task<SupportAgent?> GetLeastBusyAgentAsync()
    {
        return await _dbSet
            .Where(sa => sa.IsOnline && sa.IsAvailable &&
                        sa.CurrentActiveChats < sa.MaxConcurrentChats)
            .OrderBy(sa => sa.CurrentActiveChats)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAgentStatusAsync(int agentId, bool isOnline, bool isAvailable)
    {
        var agent = await _dbSet.FindAsync(agentId);
        if (agent != null)
        {
            agent.IsOnline = isOnline;
            agent.IsAvailable = isAvailable;
            agent.LastSeenAt = DateTime.UtcNow;
            _context.Update(agent);
        }
    }

    public async Task IncrementActiveChatCountAsync(int agentId)
    {
        var agent = await _dbSet.FindAsync(agentId);
        if (agent != null)
        {
            agent.CurrentActiveChats++;
            _context.Update(agent);
        }
    }

    public async Task DecrementActiveChatCountAsync(int agentId)
    {
        var agent = await _dbSet.FindAsync(agentId);
        if (agent != null && agent.CurrentActiveChats > 0)
        {
            agent.CurrentActiveChats--;
            _context.Update(agent);
        }
    }
}
