namespace AiFleet.Core.Interfaces;

using System;

public interface IAiFleetClock
{
    DateTime UtcNow { get; }
}