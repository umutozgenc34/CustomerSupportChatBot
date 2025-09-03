using CustomerSupportChatBot.Core.Dtos.Responses;
using CustomerSupportChatBot.Core.Enums;

namespace CustomerSupportChatBot.Core.Interfaces.Services;

public interface INLPService
{
    Task<NLPAnalysisResponse> AnalyzeMessageAsync(string message);
    Task<IntentType> DetectIntentAsync(string message);
    Task<double> CalculateConfidenceAsync(string message, IntentType intent);
    Task<IEnumerable<string>> ExtractKeywordsAsync(string message);
    Task<Dictionary<string, object>> ExtractEntitiesAsync(string message);
    Task<bool> IsGreetingAsync(string message);
    Task<bool> IsGoodbyeAsync(string message);
    Task<bool> RequiresHumanSupportAsync(string message);
}
