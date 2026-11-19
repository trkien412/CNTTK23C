using Microsoft.AspNetCore.SignalR;

namespace Ping.Api.Hubs;

public class PingHub : Hub
{
    public async Task Echo(string message)
    {
        await Clients.Caller.SendAsync("EchoResponse", message);
    }
}