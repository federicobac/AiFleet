namespace AiFleet.UnitTests;

using Xunit;
using AiFleet.Core;

public class BasicAiFleetServiceTests
{
    // Ex 1: Simple Token Addition
    [Fact]
    public void CombineTokenCounts_TwoPromptCounts_ReturnsSum()
    {
        int result = BasicAiFleetService.CombineTokenCounts(1500, 2500);
        Assert.Equal(4000, result);
    }

    // Ex 2: Time Unit Conversion
    [Fact]
    public void SecondsToExecutionMinutes_ThreeHundredSeconds_ReturnsFiveMinutes()
    {
        int result = BasicAiFleetService.SecondsToExecutionMinutes(300);
        Assert.Equal(5, result);
    }

    // Ex 3: Boolean Threshold Check
    [Theory]
    [InlineData(100.0, true)]
    [InlineData(10.0, true)]
    [InlineData(9.9, false)]
    [InlineData(0.0, false)]
    public void HasSufficientCredits_ChecksTenCreditThreshold(double creditBalance, bool expected)
    {
        bool result = BasicAiFleetService.HasSufficientCredits(creditBalance);
        Assert.Equal(expected, result);
    }

    // Ex 4: Basic Multiplication
    [Fact]
    public void CalculateStandardComputeCost_TenMinutes_ReturnsFiveDollars()
    {
        // $0.50 per minute standard compute rate
        decimal result = BasicAiFleetService.CalculateStandardComputeCost(10);
        Assert.Equal(5.00m, result);
    }

    // Ex 5: String Formatting
    [Fact]
    public void FormatAgentCallsign_IdAndModel_ReturnsFormattedString()
    {
        string result = BasicAiFleetService.FormatAgentCallsign(7, "Claude-3.5-Sonnet");
        Assert.Equal("AGENT-7: Claude-3.5-Sonnet", result);
    }
}