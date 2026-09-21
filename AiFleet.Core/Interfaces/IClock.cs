namespace AiFleet.Core.Interfaces;

public interface IClock
{
    DateTime UtcNow { get; }
}