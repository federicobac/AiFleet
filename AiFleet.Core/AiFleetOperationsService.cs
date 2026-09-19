namespace AiFleet.Core;

public class AiFleetOperationsService
{
    //Exercise 6
    public static decimal CalculateTokenCost(int input, int output)
    {
        return (input / 1000m * 0.01m) + (output / 1000m * 0.03m);
    }

    //Exercise 7
    public static bool WillFitInContextWindow(int current, int prompt, int limit)
    {
        // if (current + prompt > limit)
        //     return false;
        //
        // return true;
        
        return current + prompt <= limit;
    }

    //Exercise 8
    public static string GetRateLimitsStatus(int used, int limit)
    {
        if (used >= (limit * 0.9))
            return $"WARNING: NEAR TPM LIMIT";

        return $"OK";
    }

    //Exercise 9
    public static int EstimateTokenCount(string? text)
    {
        if (string.IsNullOrEmpty(text))
            return 0;
        
        return (int)Math.Ceiling(text.Length / 4.0m);
    }

    //Exercise 10
    public static string SelectModelTier(int rating)
    {
        // if (rating <= 0)
        //     return "UNKNOWN";
        //
        // if (rating <= 3)
        //     return "MINI";
        //
        // return "REASONING";

        return rating switch
        {
            <= 0 => "UNKNOWN",
            <= 3 => "MINI",
            _ => "REASONING"
        };
    }

    //Exercise 11
    public static decimal CalculateCachedPromptCost(int totalTokens, bool isCached)
    {
        decimal cost = totalTokens / 1000m * 0.02m;

        if (isCached)
            cost *= 0.5m;

        return cost;
    }
    
    //Exercise 12
    public static bool ShouldCompressContext(int currentTokens, int maxLimit)
    {
        return currentTokens >= maxLimit * 0.8m;
    }
}