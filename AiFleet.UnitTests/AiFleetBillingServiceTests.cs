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
            UtcNow = new DateTime(2026, 09, 21, hour, 0, 0)
        };

        var service = new AiFleetBillingService(clock);
        decimal result = service.CalculateBatchJobCost(100000);
        Assert.Equal(expected, result);
    }
    
    //Exercise 18
    [Theory]
    [InlineData(19, 12.00)] //saturday
    [InlineData(20, 12.00)] //sunday
    [InlineData(18, 2.00)] //friday
    [InlineData(21, 2.00)] //monday
    public void CalculateBatchJobCost_AppliesWeekendSurcharge(
        int day, decimal expected)
    {
        var clock = new FakeClock
        {
            UtcNow = new DateTime(2026, 09, day, 14, 0, 0)
        };
        
        var service = new AiFleetBillingService(clock);
        decimal result = service.CalculateBatchJobCost(100000);
        Assert.Equal(expected, result);
    }
    
    //Exercise 19
    [Theory]
    [InlineData(30, true)]
    [InlineData(15, false)]
    public void AreUnusedCreditsExpired_ReturnsCorrectResult(
        int day, bool expected)
    {
        var clock = new FakeClock
        {
            UtcNow = new DateTime(2026, 09, day, 14, 0, 0)
        };
        
        var service = new AiFleetBillingService(clock);
        bool result = service.AreUnusedCreditsExpired();
        Assert.Equal(expected, result);
    }
    
    //Exercise 20
    [Theory]
    [InlineData(21, 15, 15.00)] //Monday 15:00
    [InlineData(22, 16, 15.00)] //Tuesday 16:00
    [InlineData(23, 17, 15.00)] //Wednesday 17:00
    [InlineData(24, 14, 15.00)] //Thursday 14:00
    [InlineData(25, 13, 10.00)] //Friday 10:00
    [InlineData(26, 18, 10.00)] //Saturday 18:00
    [InlineData(27, 15, 10.00)] //Sunday 15:00
    public void CalculateSurgeCost_AppliesWeejdaySurge(
        int day, int hour, decimal expected)
    {
        var clock = new FakeClock()
        {
            UtcNow = new DateTime(2026, 09, day, hour, 0, 0)
        };

        var service = new AiFleetBillingService(clock);
        decimal result = service.CalculateSurgeCost(10m);
        Assert.Equal(expected, result);
    }

}


public class FakeClock : IClock
{
    public DateTime UtcNow { get; set; }
}