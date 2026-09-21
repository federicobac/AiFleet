namespace AiFleet.UnitTests.Stubs;

using System;
using System.Collections.Generic;
using AiFleet.Core.Interfaces;

public class FeatureFlagServiceStub : IFeatureFlagService
{
    public Dictionary<string, bool> Flags { get; set; } = new();

    public bool IsFeatureEnabled(string featureName)
    {
        return Flags.TryGetValue(featureName, out bool enabled) && enabled;
    }
}