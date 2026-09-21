using AiFleet.Core.Interfaces;

namespace AiFleet.Core;

//Exercise 17
public class AiFleetBillingService
{
    private readonly IClock _clock;

    public AiFleetBillingService(IClock clock)
    {
        _clock = clock;
    }
    
    public decimal CalculateBatchJobCost(int totalTokens)
    {
        decimal cost = totalTokens / 1000m * 0.02m;
        
        int hour = _clock.UtcNow.Hour;

        if (hour >= 22 || hour < 4)
            cost *= 0.5m;

        if (_clock.UtcNow.DayOfWeek == DayOfWeek.Saturday ||
            _clock.UtcNow.DayOfWeek == DayOfWeek.Sunday)
        {
            cost += 10m;
        }

        return cost;
    }
}