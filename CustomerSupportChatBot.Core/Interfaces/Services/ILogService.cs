using CustomerSupportChatBot.Core.Enums;

namespace CustomerSupportChatBot.Core.Interfaces.Services;

public interface ILogService
{
    Task LogChatInteractionAsync(int sessionId, string action, object? data = null);
    Task LogErrorAsync(string message, Exception exception, object? context = null);
    Task LogNLPAnalysisAsync(string message, IntentType detectedIntent, double confidence);
    Task LogAutoResponseUsageAsync(int responseId, int sessionId, bool wasSuccessful);
    Task LogAgentTransferAsync(int sessionId, int? agentId, string reason);
    Task LogSystemEventAsync(string eventType, object? data = null);
    Task<IEnumerable<object>> GetChatLogsAsync(int sessionId);
    Task<IEnumerable<object>> GetErrorLogsAsync(DateTime? startDate = null, DateTime? endDate = null);
}
