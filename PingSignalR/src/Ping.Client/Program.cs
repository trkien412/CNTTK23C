using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Dang ket noi den SignalR Server...");

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5282/pingHub")
    .WithAutomaticReconnect()
    .Build();

connection.Reconnecting += error =>
{
    Console.WriteLine("Dang ket noi lai...");
    return Task.CompletedTask;
};

connection.Reconnected += connectionId =>
{
    Console.WriteLine("Da ket noi lai");
    return Task.CompletedTask;
};

connection.Closed += error =>
{
    Console.WriteLine("Da ngat ket noi!");
    return Task.CompletedTask;
};

connection.On<string>("EchoResponse", message =>
{
    Console.WriteLine($"[Echo] Echo: {message}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Da ket noi SignalR Server!");

    await connection.InvokeAsync("Echo", "Xin chao SignalR");
}
catch (Exception ex)
{
    Console.WriteLine($"Loi ket noi: {ex.Message}");
}

Console.ReadLine();