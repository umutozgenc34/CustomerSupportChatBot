namespace CustomerSupportChatBot.Infrastructure.External.NLP;

public class KeywordMatcher
{
    private readonly Dictionary<string, string[]> _keywordGroups = new()
    {
        ["greeting"] = new[] { "merhaba", "selam", "hello", "hi", "hey", "iyi günler", "günaydın", "iyi akşamlar" },
        ["goodbye"] = new[] { "hoşçakal", "görüşürüz", "bye", "goodbye", "see you", "güle güle", "iyi günler" },
        ["question"] = new[] { "nasıl", "ne", "kim", "nerede", "ne zaman", "neden", "hangi", "how", "what", "where", "when", "why", "which" },
        ["complaint"] = new[] { "şikayet", "sorun", "problem", "hata", "çalışmıyor", "bozuk", "complaint", "issue", "bug", "error", "broken" },
        ["request"] = new[] { "istiyorum", "lütfen", "rica", "yardım", "destek", "want", "please", "help", "support", "need" },
        ["technical"] = new[] { "teknik", "sistem", "yazılım", "uygulama", "site", "web", "technical", "system", "software", "app", "website" },
        ["billing"] = new[] { "fatura", "ödeme", "para", "ücret", "fiyat", "billing", "payment", "money", "cost", "price" },
        ["product"] = new[] { "ürün", "hizmet", "özellik", "product", "service", "feature" },
        ["order"] = new[] { "sipariş", "satın alma", "alış", "order", "purchase", "buy" },
        ["return"] = new[] { "iade", "geri", "değişim", "return", "refund", "exchange" }
    };

    public IEnumerable<string> ExtractKeywords(string message)
    {
        var keywords = new HashSet<string>();
        var messageLower = message.ToLower();

        foreach (var group in _keywordGroups)
        {
            foreach (var keyword in group.Value)
            {
                if (messageLower.Contains(keyword))
                {
                    keywords.Add(keyword);
                }
            }
        }

        return keywords;
    }

    public string? GetKeywordGroup(string message)
    {
        var messageLower = message.ToLower();

        foreach (var group in _keywordGroups)
        {
            if (group.Value.Any(keyword => messageLower.Contains(keyword)))
            {
                return group.Key;
            }
        }

        return null;
    }

    public double CalculateMatchScore(string message, string[] keywords)
    {
        if (!keywords.Any()) return 0.0;

        var messageLower = message.ToLower();
        var matches = keywords.Count(keyword => messageLower.Contains(keyword.ToLower()));

        return (double)matches / keywords.Length;
    }
}