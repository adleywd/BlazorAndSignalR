using BlazorSignalR.Api;
using BlazorSignalR.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddHostedService<ServerTimeNotifier>();

builder.Services.AddCors();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapHub<NotificationsHub>("notifications");

app.Run();
