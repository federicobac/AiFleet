public interface INotificationWebhook
{
    bool SendAlert(string channel, string message);
}