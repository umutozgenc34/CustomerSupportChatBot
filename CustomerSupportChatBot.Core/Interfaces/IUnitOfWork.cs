using CustomerSupportChatBot.Core.Interfaces.Repositories;

namespace CustomerSupportChatBot.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IChatSessionRepository ChatSessions { get; }
    IChatMessageRepository ChatMessages { get; }
    ICustomerRepository Customers { get; }
    IAutoResponseRepository AutoResponses { get; }
    ISupportAgentRepository SupportAgents { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}