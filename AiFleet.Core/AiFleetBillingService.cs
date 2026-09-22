using AiFleet.Core.Interfaces;

namespace AiFleet.Core;

//Exercise 17 + 18
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

    //Exercise 19
    public bool AreUnusedCreditsExpired()
    {
        var today = _clock.UtcNow.Date;
        int lastDay = DateTime.DaysInMonth(today.Year, today.Month);
        return today.Day == lastDay;
    }

    //Exercise 20
    public decimal CalculateSurgeCost(decimal baseCost)
    {
        var now = _clock.UtcNow;

        //is it a weekday?
        bool isWeekday =
            now.DayOfWeek >= DayOfWeek.Monday &&
            now.DayOfWeek <= DayOfWeek.Friday;

        //is it between 14:00 and 17:00?
        bool isSurgePeriod =
            now.Hour >= 14 && now.Hour < 18;

        if (isWeekday && isSurgePeriod)
            return baseCost * 1.5m;

        return baseCost;
    }
}