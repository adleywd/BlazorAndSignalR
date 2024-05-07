namespace BlazorSignalR.Api.Hubs;

public interface INotificationClient
{
    Task ReceiveNotification(string message);
}