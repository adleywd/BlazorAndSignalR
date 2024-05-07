using BlazorSignalR.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalR.Api;

public class ServerTimeNotifier : BackgroundService
{
    private static readonly TimeSpan _period = TimeSpan.FromSeconds(5);
    private readonly ILogger<ServerTimeNotifier> _logger;
    private readonly IHubContext<NotificationsHub, INotificationClient> _hubContext;

    public ServerTimeNotifier(ILogger<ServerTimeNotifier> logger, IHubContext<NotificationsHub, INotificationClient> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested
               && await timer.WaitForNextTickAsync(stoppingToken).ConfigureAwait(false))
        {
            var dateTime = DateTime.Now;
            
            _logger.LogInformation("Sending server time to clients: {DateTime}", dateTime);

            await _hubContext.Clients.All.ReceiveNotification($"Server time: {dateTime}").ConfigureAwait(false);
        }
    }
}