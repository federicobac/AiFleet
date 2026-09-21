namespace AiFleet.UnitTests;

using Xunit;
using AiFleet.Core.Demo;

public class DateTimeProviderStub : IDateTimeProvider
{
    public DateTime FakeNow { get; set; }
    public DateTime Now => FakeNow;
}



public class Ex1
{
    [Theory]
    [InlineData(17, true)]
    [InlineData(18, true)]
    [InlineData(16, false)]
    [InlineData(19, false)]
    public void IsHappyHour_ValidatesTimeBoundary(int hour, bool expectedIsHappyHour)
    {
        var stub = new DateTimeProviderStub
        {
            FakeNow = new DateTime(2026, 9, 21, hour, 30, 0)
        };

        var engine = new DiscountEngine(stub);

        bool result = engine.IsHappyHour();

        Assert.Equal(expectedIsHappyHour, result);
    }
}