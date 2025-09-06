using CustomerSupportChatBot.Core.Interfaces.Repositories;
using CustomerSupportChatBot.Core.Interfaces;
using CustomerSupportChatBot.Infrastructure.Context;
using CustomerSupportChatBot.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerSupportChatBot.Infrastructure.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    private IChatSessionRepository? _chatSessions;
    private IChatMessageRepository? _chatMessages;
    private ICustomerRepository? _customers;
    private IAutoResponseRepository? _autoResponses;
    private ISupportAgentRepository? _supportAgents;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IChatSessionRepository ChatSessions =>
        _chatSessions ??= new ChatSessionRepository(_context);

    public IChatMessageRepository ChatMessages =>
        _chatMessages ??= new ChatMessageRepository(_context);

    public ICustomerRepository Customers =>
        _customers ??= new CustomerRepository(_context);

    public IAutoResponseRepository AutoResponses =>
        _autoResponses ??= new AutoResponseRepository(_context);

    public ISupportAgentRepository SupportAgents =>
        _supportAgents ??= new SupportAgentRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
