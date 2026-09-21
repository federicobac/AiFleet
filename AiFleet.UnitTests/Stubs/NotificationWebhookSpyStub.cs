namespace AiFleet.UnitTests.Stubs;

using System;
using System.Collections.Generic;
using AiFleet.Core.Interfaces;
public class NotificationWebhookSpyStub : INotificationWebhook
{
    public int CallCount { get; private set; }
    public string? LastChannelSent { get; private set; }
    public string? LastMessageSent { get; private set; }

    public bool SendAlert(string channel, string message)
    {
        CallCount++;
        LastChannelSent = channel;
        LastMessageSent = message;
        return true;
    }
}