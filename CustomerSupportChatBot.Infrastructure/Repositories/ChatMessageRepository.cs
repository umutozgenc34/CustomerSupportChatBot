using CustomerSupportChatBot.Core.Entities;
using CustomerSupportChatBot.Core.Enums;
using CustomerSupportChatBot.Core.Interfaces.Repositories;
using CustomerSupportChatBot.Infrastructure.Context;
using CustomerSupportChatBot.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportChatBot.Infrastructure.Repositories;

public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ChatMessage>> GetMessagesBySessionAsync(int sessionId)
    {
        return await _dbSet
            .Where(cm => cm.ChatSessionId == sessionId)
            .OrderBy(cm => cm.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ChatMessage>> GetUnreadMessagesAsync(int sessionId)
    {
        return await _dbSet
            .Where(cm => cm.ChatSessionId == sessionId && !cm.IsRead)
            .OrderBy(cm => cm.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ChatMessage>> GetMessagesByTypeAsync(MessageType messageType)
    {
        return await _dbSet
            .Where(cm => cm.MessageType == messageType)
            .OrderByDescending(cm => cm.CreatedAt)
            .ToListAsync();
    }

    public async Task<ChatMessage?> GetLastMessageBySessionAsync(int sessionId)
    {
        return await _dbSet
            .Where(cm => cm.ChatSessionId == sessionId)
            .OrderByDescending(cm => cm.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ChatMessage>> GetMessagesByIntentAsync(IntentType intentType)
    {
        return await _dbSet
            .Where(cm => cm.DetectedIntent == intentType)
            .OrderByDescending(cm => cm.CreatedAt)
        .ToListAsync();
    }

    public async Task<int> GetUnreadMessageCountAsync(int sessionId)
    {
        return await _dbSet
            .CountAsync(cm => cm.ChatSessionId == sessionId && !cm.IsRead);
    }

    public async Task MarkMessagesAsReadAsync(int sessionId)
    {
        var unreadMessages = await _dbSet
            .Where(cm => cm.ChatSessionId == sessionId && !cm.IsRead)
            .ToListAsync();

        foreach (var message in unreadMessages)
        {
            message.IsRead = true;
            message.ReadAt = DateTime.UtcNow;
        }

        _context.UpdateRange(unreadMessages);
    }
}
