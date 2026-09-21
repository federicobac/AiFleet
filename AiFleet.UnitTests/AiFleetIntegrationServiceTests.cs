namespace AiFleet.UnitTests;

using Xunit;
using AiFleet.Core.Services;
using AiFleet.UnitTests.Stubs;

public class AiFleetIntegrationServiceTests
{
    [Fact]
    public void GenerateAgentApiKey_CombinesAgentNameWithGeneratedKey()
    {
        var stub = new FixedKeyGeneratorStub { FixedKeyToReturn = "ABC-999" };
        var service = new AiFleetIntegrationService(keyGenerator: stub);

        string result = service.GenerateAgentApiKey("AlphaAgent");

        Assert.Equal("AlphaAgent-KEY-ABC-999", result);
    }

    [Fact]
    public void NotifyWorkspaceLimitReached_InvokesWebhookAndRecordsInSpy()
    {
        var spy = new NotificationWebhookSpyStub();
        var service = new AiFleetIntegrationService(webhook: spy);

        bool success = service.NotifyWorkspaceLimitReached("WS-FULL");

        Assert.True(success);
        Assert.Equal(1, spy.CallCount);
        Assert.Equal("slack-alerts", spy.LastChannelSent);
        Assert.Equal("Workspace WS-FULL is full!", spy.LastMessageSent);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void CanUseReasoningModel_ChecksFeatureFlagService_ReturnsFlagState(bool flagState, bool expectedResult)
    {
        var stub = new FeatureFlagServiceStub();
        stub.Flags["EnableReasoningModels"] = flagState;

        var service = new AiFleetIntegrationService(featureFlags: stub);
        bool canUse = service.CanUseReasoningModel("WS-01");

        Assert.Equal(expectedResult, canUse);
    }

    [Theory]
    [InlineData(100.00, "EUR", 108.00)]
    [InlineData(100.00, "GBP", 125.00)]
    public void ConvertInvoiceToCurrency_MultipliesByProviderRate(decimal usdAmount, string currency, decimal expectedConverted)
    {
        var stub = new FixedExchangeRateStub();
        stub.Rates["EUR"] = 1.08m;
        stub.Rates["GBP"] = 1.25m;

        var service = new AiFleetIntegrationService(exchangeRateProvider: stub);
        decimal converted = service.ConvertInvoiceToCurrency(usdAmount, currency);

        Assert.Equal(expectedConverted, converted);
    }

    [Fact]
    public void RegisterWithRetry_FlakyService_RetriesAndSucceedsOnSecondAttempt()
    {
    }

    [Theory]
    [InlineData(true, "https://primary.ai")]
    [InlineData(false, "https://fallback.ai")]
    public void ResolveEndpoint_EvaluatesPrimaryHealth_RoutesToCorrectEndpoint(bool isPrimaryHealthy, string expectedEndpoint)
    {}

    [Theory]
    [InlineData(60000, 50000, 1)]
    [InlineData(40000, 50000, 0)]
    public void EvaluateUsageAndAlert_SendsEmailOnlyWhenExceedingThreshold(int tokensUsed, int threshold, int expectedDispatches)
    {}

    [Theory]
    [InlineData(75.5, true)]
    [InlineData(82.0, false)]
    public void CanRunInference_ValidatesGpuTemperatureThreshold(double gpuTemp, bool expectedResult)
    {}
}