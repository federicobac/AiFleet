namespace AiFleet.UnitTests.Stubs;

using System;
using AiFleet.Core.Interfaces;

public class FixedAiFleetClockStub : IAiFleetClock
{
    public DateTime UtcNow { get; set; }

    public FixedAiFleetClockStub(DateTime fixedTime)
    {
        UtcNow = fixedTime;
    }
}