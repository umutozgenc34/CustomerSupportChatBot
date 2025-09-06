using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Interfaces.Repositories;
using CustomerSupportChatBot.Infrastructure.Context;
using CustomerSupportChatBot.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetBySessionIdAsync(string sessionId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.SessionId == sessionId);
    }

    public async Task<Customer?> GetCustomerWithSessionsAsync(int customerId)
    {
        return await _dbSet
            .Include(c => c.ChatSessions)
            .FirstOrDefaultAsync(c => c.Id == customerId);
    }

    public async Task<IEnumerable<Customer>> GetCustomersByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(c => c.CreatedAt >= startDate && c.CreatedAt <= endDate)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}