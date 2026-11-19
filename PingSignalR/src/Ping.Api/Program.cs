using Ping.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/", () => "SignalR Server is running!");

app.MapHub<PingHub>("/pingHub");

app.Run();