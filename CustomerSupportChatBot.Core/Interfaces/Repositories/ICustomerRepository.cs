using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Interfaces.Repositories.Base;

namespace CustomerSupportChatBot.Core.Interfaces.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByEmailAsync(string email);
    Task<Customer?> GetBySessionIdAsync(string sessionId);
    Task<Customer?> GetCustomerWithSessionsAsync(int customerId);
    Task<IEnumerable<Customer>> GetCustomersByDateRangeAsync(DateTime startDate, DateTime endDate);
}