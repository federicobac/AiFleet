namespace AiFleet.UnitTests.Stubs;

using System;
using System.Collections.Generic;
using AiFleet.Core.Interfaces;

public class FixedKeyGeneratorStub : IRandomKeyGenerator
{
    public string FixedKeyToReturn { get; set; } = "FIXED-12345";

    public string GenerateKey(string prefix)
    {
        return $"{prefix}-{FixedKeyToReturn}";
    }
}