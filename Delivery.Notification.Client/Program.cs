using System.Net.Http.Json;
using System.Text.Json;
using Delivery.Common.DTO;
using Microsoft.AspNetCore.SignalR.Client;

var loginCredentials = new Dictionary<string, string?>()
    { { "email", "user@example.com" }, { "password", "P@ssw0rd" } };

Console.WriteLine("Do you want to login with your credentials? (y/n)");
if (Console.ReadKey().Key == ConsoleKey.Y) {
    Console.WriteLine();
    Console.Write("email >> ");
    loginCredentials["email"] = Console.ReadLine();
    Console.Write("password >> ");
    loginCredentials["password"] = Console.ReadLine();
}

Console.WriteLine();

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5144/api/notifications", options => { options.AccessTokenProvider = Login; })
    .WithAutomaticReconnect()
    .Build();

void Print(string message) {
    var obj = JsonSerializer.Deserialize<MessageDto>(message);
    if (obj == null) {
        Console.WriteLine("Error deserializing message");
        return;
    }

    Console.WriteLine($"Title: {obj.Title}  Text: {obj.Text}  CreatedAt: {obj.CreatedAt}");
}

connection.On("ReceiveMessage", (Action<string>)Print);

async Task<string?> Login() {
    HttpClient client = new HttpClient();
    var loginResult = await client.PostAsJsonAsync("http://localhost:5298/api/login", loginCredentials);
    if (loginResult.IsSuccessStatusCode) {
        var content = await loginResult.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<Dictionary<string, string>>(content)?["accessToken"];
        return token;
    }

    return null;
}

try {
    await connection.StartAsync();
}
catch (Exception e) {
    Console.WriteLine(e.Message);
    return;
}

Console.WriteLine(connection.State == HubConnectionState.Connected
    ? "Connected to notification hub"
    : "Error connecting to notification hub");
while (true) {
    var _ = Console.ReadLine();
}