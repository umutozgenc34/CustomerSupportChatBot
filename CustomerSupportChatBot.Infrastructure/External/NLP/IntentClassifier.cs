using CustomerSupportChatBot.Core.Enums;
using System.Text.RegularExpressions;

namespace CustomerSupportChatBot.Infrastructure.External.NLP;

public class IntentClassifier
{
    private readonly KeywordMatcher _keywordMatcher;

    private readonly Dictionary<IntentType, string[]> _intentPatterns = new()
    {
        [IntentType.Greeting] = new[]
        {
            @"^(merhaba|selam|hello|hi|hey)",
            @"(iyi günler|günaydın|iyi akşamlar)",
            @"^(good morning|good afternoon|good evening)"
        },
        [IntentType.Goodbye] = new[]
        {
            @"(hoşçakal|görüşürüz|bye|goodbye)",
            @"(güle güle|iyi günler|see you)",
            @"(teşekkür.*|thank.*you)"
        },
        [IntentType.Question] = new[]
        {
            @"(nasıl|ne|kim|nerede|ne zaman|neden|hangi)",
            @"(how|what|where|when|why|which|who)",
            @"\?"
        },
        [IntentType.Complaint] = new[]
        {
            @"(şikayet|sorun|problem|hata)",
            @"(çalışmıyor|bozuk|complaint|issue|bug)",
            @"(kötü|berbat|awful|terrible)"
        }
    };

    public IntentClassifier(KeywordMatcher keywordMatcher)
    {
        _keywordMatcher = keywordMatcher;
    }

    public IntentType ClassifyIntent(string message)
    {
        var messageLower = message.ToLower().Trim();

        foreach (var intentPattern in _intentPatterns)
        {
            foreach (var pattern in intentPattern.Value)
            {
                if (Regex.IsMatch(messageLower, pattern, RegexOptions.IgnoreCase))
                {
                    return intentPattern.Key;
                }
            }
        }

        var keywordGroup = _keywordMatcher.GetKeywordGroup(message);

        return keywordGroup switch
        {
            "greeting" => IntentType.Greeting,
            "goodbye" => IntentType.Goodbye,
            "question" => IntentType.Question,
            "complaint" => IntentType.Complaint,
            "request" => IntentType.Request,
            "technical" => IntentType.TechnicalSupport,
            "billing" => IntentType.BillingInquiry,
            "product" => IntentType.ProductInfo,
            "order" => IntentType.OrderStatus,
            "return" => IntentType.Returns,
            _ => IntentType.Unknown
        };
    }

    public double CalculateConfidence(string message, IntentType intent)
    {
        var messageLower = message.ToLower();
        var matchCount = 0;
        var totalPatterns = 0;

        if (_intentPatterns.ContainsKey(intent))
        {
            var patterns = _intentPatterns[intent];
            totalPatterns = patterns.Length;

            matchCount = patterns.Count(pattern =>
                Regex.IsMatch(messageLower, pattern, RegexOptions.IgnoreCase));
        }

        var keywordGroup = GetKeywordGroupForIntent(intent);
        if (keywordGroup != null)
        {
            var keywordMatch = _keywordMatcher.GetKeywordGroup(message);
            if (keywordMatch == keywordGroup)
            {
                matchCount++;
            }
            totalPatterns++;
        }

        return totalPatterns > 0 ? (double)matchCount / totalPatterns : 0.0;
    }

    private string? GetKeywordGroupForIntent(IntentType intent)
    {
        return intent switch
        {
            IntentType.Greeting => "greeting",
            IntentType.Goodbye => "goodbye",
            IntentType.Question => "question",
            IntentType.Complaint => "complaint",
            IntentType.Request => "request",
            IntentType.TechnicalSupport => "technical",
            IntentType.BillingInquiry => "billing",
            IntentType.ProductInfo => "product",
            IntentType.OrderStatus => "order",
            IntentType.Returns => "return",
            _ => null
        };
    }
}