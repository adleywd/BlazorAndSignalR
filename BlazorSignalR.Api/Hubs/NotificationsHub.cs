using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalR.Api.Hubs;

public class NotificationsHub : Hub<INotificationClient>
{
    public override Task OnConnectedAsync()
    {
        Clients.Client(Context.ConnectionId).ReceiveNotification($"Welcome! {Context.User?.Identity?.Name}");
        
        return base.OnConnectedAsync();
    }
}