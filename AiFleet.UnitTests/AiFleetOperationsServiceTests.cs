namespace AiFleet.UnitTests;

using Xunit;
using AiFleet.Core;

public class AiFleetOperationsServiceTests
{
    //Exercise 6
    [Theory]
    [InlineData(100000, 50000, 2.50)]
    [InlineData(0, 0, 0.00)]
    public void CalculateTokenCost_CalculatesAsymmetricRates(int input, int output, decimal expected)
    {
        decimal result = AiFleetOperationsService.CalculateTokenCost(input, output);
        Assert.Equal(expected, result);
    }
    
    //Exercise 7
    [Theory]
    [InlineData(115000, 10000, 128000, true)]
    [InlineData(115000, 20000, 128000, false)]
    public void WillFitInContextWindow_ValidatesMaxLimit(int current, int prompt, int limit, bool expected)
    {
        bool result = AiFleetOperationsService.WillFitInContextWindow(current, prompt, limit);
        Assert.Equal(expected, result);
    }
    
    //Exercise 8
    [Theory]
    [InlineData(90000, 100000, "WARNING: NEAR TPM LIMIT")]
    [InlineData(85000, 100000, "OK")]
    public void GetRateLimitStatus_TriggersAtNinetyPercent(int used, int limit, string expected)
    {
        string result = AiFleetOperationsService.GetRateLimitsStatus(used, limit);
        Assert.Equal(expected, result);
    }
    
    //Exercise 9
    [Theory]
    [InlineData("Hello World 1234", 4)] // 16 chars -> 4 tokens
    [InlineData("Hello World 12345", 5)] // 17 chars -> 5 tokens (rounded up)
    [InlineData("", 0)]
    public void EstimateTokenCount_RoundsUpCorrectly(string text, int expected)
    {
        int result = AiFleetOperationsService.EstimateTokenCount(text);
        Assert.Equal(expected, result);
    }
    
    //Exercise 10
    [Theory]
    [InlineData(2, "MINI")]
    [InlineData(5, "REASONING")]
    [InlineData(0, "UNKNOWN")]
    public void SelectModelTier_RoutesBasedOnDifficulty(int rating, string expected)
    {
        string result = AiFleetOperationsService.SelectModelTier(rating);
        Assert.Equal(expected, result);
    }
    
    //Exercise 11
    [Theory]
    [InlineData(1000, false, 0.02)]
    [InlineData(1000, true, 0.01)]
    [InlineData(100000, false, 2.00)]
    [InlineData(100000, true, 1.00)]
    public void CalculateCachedPromptCost_AppliesCacheDiscount(
        int totalTokens, bool isCached, decimal expected)
    {
        decimal result = AiFleetOperationsService.CalculateCachedPromptCost(
            totalTokens, isCached);
        
        Assert.Equal(expected, result);
    }

    //Exercise 12
    [Theory]
    [InlineData(800, 1000, true)]
    [InlineData(900, 1000, true)]
    [InlineData(799, 1000, false)]
    [InlineData(500, 1000, false)]
    public void ShouldCompressContext_TriggersAtEightyPercent(
        int currentTokens, int maxLimit, bool expected)
    {
        bool result =
            AiFleetOperationsService.ShouldCompressContext(currentTokens, maxLimit);
        
        Assert.Equal(expected, result);
    }
    
    //Exercise 13
    [Theory]
    [InlineData(10, "ENTERPRISE", true)]
    [InlineData(11, "ENTERPRISE", false)]
    [InlineData(4, "PRO", true)]
    [InlineData(5, "PRO", false)]
    [InlineData(1, "FREE", true)]
    [InlineData(2, "FREE", false)]
    public void CanExecuteParallelTools_RespectPlanLimits(
        int requestedTools, string planTier, bool expected)
    {
        bool result = AiFleetOperationsService.CanExecuteParallelTools(requestedTools, planTier);
        Assert.Equal(expected, result);
    }
    
    //Exercise 14
    [Theory]
    [InlineData(0, 0)]
    [InlineData(4, 0)]
    [InlineData(5, 30)]
    [InlineData(10, 30)]
    [InlineData(11, 120)]
    public void GetCoolDownSeconds_ReturnsCorrectCooldown(
        int consecutiveExecutions, int expected)
    {
        int result = AiFleetOperationsService.GetCoolDownSeconds(consecutiveExecutions);
        Assert.Equal(expected, result);
    }
}