using CustomerSupportChatBot.Core.Dtos.Responses;
using CustomerSupportChatBot.Core.Enums;
using CustomerSupportChatBot.Core.Interfaces.Services;
using System.Text.RegularExpressions;

namespace CustomerSupportChatBot.Infrastructure.External.NLP;

public class NLPService : INLPService
{
    private readonly KeywordMatcher _keywordMatcher;
    private readonly IntentClassifier _intentClassifier;

    public NLPService()
    {
        _keywordMatcher = new KeywordMatcher();
        _intentClassifier = new IntentClassifier(_keywordMatcher);
    }

    public async Task<NLPAnalysisResponse> AnalyzeMessageAsync(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return new NLPAnalysisResponse
            {
                OriginalMessage = message,
                DetectedIntent = IntentType.Unknown,
                Confidence = 0.0,
                Keywords = new List<string>(),
                ExtractedEntities = new Dictionary<string, object>()
            };
        }


        var detectedIntent = await DetectIntentAsync(message);
        var confidence = await CalculateConfidenceAsync(message, detectedIntent);

        var keywords = await ExtractKeywordsAsync(message);

        var entities = await ExtractEntitiesAsync(message);

        var isGreeting = await IsGreetingAsync(message);
        var isGoodbye = await IsGoodbyeAsync(message);
        var requiresHuman = await RequiresHumanSupportAsync(message);

        return new NLPAnalysisResponse
        {
            OriginalMessage = message,
            DetectedIntent = detectedIntent,
            Confidence = confidence,
            Keywords = keywords,
            ExtractedEntities = entities,
            IsGreeting = isGreeting,
            IsGoodbye = isGoodbye,
            RequiresHumanSupport = requiresHuman,
            SuggestedResponse = GenerateSuggestedResponse(detectedIntent, message)
        };
    }

    public async Task<IntentType> DetectIntentAsync(string message)
    {
        await Task.CompletedTask; 
        return _intentClassifier.ClassifyIntent(message);
    }

    public async Task<double> CalculateConfidenceAsync(string message, IntentType intent)
    {
        await Task.CompletedTask;
        return _intentClassifier.CalculateConfidence(message, intent);
    }

    public async Task<IEnumerable<string>> ExtractKeywordsAsync(string message)
    {
        await Task.CompletedTask;
        return _keywordMatcher.ExtractKeywords(message);
    }

    public async Task<Dictionary<string, object>> ExtractEntitiesAsync(string message)
    {
        await Task.CompletedTask;
        var entities = new Dictionary<string, object>();

        var emailPattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b";
        var emails = Regex.Matches(message, emailPattern, RegexOptions.IgnoreCase)
            .Cast<Match>()
            .Select(m => m.Value)
            .ToList();

        if (emails.Any())
            entities["emails"] = emails;

        var phonePattern = @"(\+90|0)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}";
        var phones = Regex.Matches(message, phonePattern)
            .Cast<Match>()
            .Select(m => m.Value.Trim())
            .ToList();

        if (phones.Any())
            entities["phones"] = phones;

        var datePattern = @"\b\d{1,2}[./]\d{1,2}[./]\d{2,4}\b";
        var dates = Regex.Matches(message, datePattern)
            .Cast<Match>()
            .Select(m => m.Value)
            .ToList();

        if (dates.Any())
            entities["dates"] = dates;

        var numberPattern = @"\b\d+\b";
        var numbers = Regex.Matches(message, numberPattern)
            .Cast<Match>()
            .Select(m => int.TryParse(m.Value, out var num) ? num : 0)
            .Where(n => n > 0)
            .ToList();

        if (numbers.Any())
            entities["numbers"] = numbers;

        return entities;
    }

    public async Task<bool> IsGreetingAsync(string message)
    {
        await Task.CompletedTask;
        var intent = await DetectIntentAsync(message);
        return intent == IntentType.Greeting;
    }

    public async Task<bool> IsGoodbyeAsync(string message)
    {
        await Task.CompletedTask;
        var intent = await DetectIntentAsync(message);
        return intent == IntentType.Goodbye;
    }

    public async Task<bool> RequiresHumanSupportAsync(string message)
    {
        await Task.CompletedTask;
        var messageLower = message.ToLower();

        var humanRequiredKeywords = new[]
        {
            "temsilci", "insan", "agent", "human", "person",
            "şikayet", "complaint", "ciddi", "serious", "urgent",
            "müdür", "manager", "supervisor", "escalate",
            "hukuki", "legal", "avukat", "lawyer",
            "iptal", "cancel", "iade", "refund"
        };

        return humanRequiredKeywords.Any(keyword => messageLower.Contains(keyword)) ||
               messageLower.Contains("canlı destek") ||
               messageLower.Contains("live support") ||
               messageLower.Contains("insan ile") ||
               messageLower.Contains("speak to human");
    }

    private string? GenerateSuggestedResponse(IntentType intent, string message)
    {
        return intent switch
        {
            IntentType.Greeting => "Merhaba! Size nasıl yardımcı olabilirim?",
            IntentType.Goodbye => "İyi günler! Başka bir sorunuz olursa her zaman buradayım.",
            IntentType.Question => "Sorunuzu anladım. Size yardımcı olmaya çalışacağım.",
            IntentType.Complaint => "Yaşadığınız sorun için özür dilerim. Konuyu detaylarıyla inceleyeceğim.",
            IntentType.Request => "Talebinizi aldım. En kısa sürede size geri dönüş yapacağım.",
            IntentType.TechnicalSupport => "Teknik sorunla ilgili size yardımcı olmaya çalışacağım.",
            IntentType.BillingInquiry => "Faturayla ilgili sorununuzu çözmek için elimden geleni yapacağım.",
            IntentType.ProductInfo => "Ürünlerimiz hakkında bilgi vermek için buradayım.",
            IntentType.OrderStatus => "Siparişinizin durumunu kontrol etmek için bilgilerinize ihtiyacım var.",
            IntentType.Returns => "İade işleminizle ilgili size yardımcı olabilirim.",
            _ => "Mesajınızı aldım. Size nasıl yardımcı olabilirim?"
        };
    }
}