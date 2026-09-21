using AiFleet.Core;
using AiFleet.Core.Interfaces;

namespace AiFleet.UnitTests;

public class AiFleetBillingServiceTests
{
    //Exercise 17
    [Theory]
    [InlineData(23, 1.00)]
    [InlineData(22, 1.00)]
    [InlineData(3, 1.00)]
    [InlineData(4, 2.00)]
    [InlineData(14, 2.00)]
    public void CalculateBatchJobCost_AppliesNightDiscount(
        int hour, decimal expected)
    {
        var clock = new FakeClock
        {
            UtcNow = new DateTime(2026, 09, 20, 23, 0, 0)
        };

        var service = new AiFleetBillingService(clock);
        decimal result = service.CalculateBatchJobCost(100000);
        Assert.Equal(1.00m, result);
    }
}

public class FakeClock : IClock
{
    public DateTime UtcNow { get; set; }
}