using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Dang ket noi den SignalR Server...");

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5282/pingHub")
    .WithAutomaticReconnect()
    .Build();

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