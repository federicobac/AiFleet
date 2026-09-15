namespace AiFleet.Core;

using System;

public class BasicAiFleetService
{
    public static int CombineTokenCounts(int tokensA, int tokensB)
    {
        return tokensA + tokensB;
    }

    public static int SecondsToExecutionMinutes(int seconds)
    {
        return seconds / 60;
    }

    public static bool HasSufficientCredits(double creditBalance)
    {
        if (creditBalance < 10.0) 
            return false;

        return true;
    }

    public static decimal CalculateStandardComputeCost(int runtimeMinutes)
    {
        return runtimeMinutes * 0.50m;
    }

    public static string FormatAgentCallsign(int agentId, string modelName)
    {
        return $"AGENT-{agentId}: {modelName}";
    } 
}