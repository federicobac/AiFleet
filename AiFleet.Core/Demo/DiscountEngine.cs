namespace AiFleet.Core.Demo;

using System;

public interface IDateTimeProvider
{
    DateTime Now { get;}
}

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}

public class DiscountEngine
{

    private readonly IDateTimeProvider _dateTimeProvider;
    public DiscountEngine(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public bool IsHappyHour()
    {
        var currentHour = _dateTimeProvider.Now.Hour;

        return currentHour >= 17 && currentHour < 19; 
    }
}