using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;

var loginCredentials = new Dictionary<string, string?>() { {"email", "user@example.com"}, {"password", "P@ssw0rd"} };

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
    .WithUrl("http://localhost:5144/api/notifications", options =>
    { 
        options.AccessTokenProvider = () => Login();
    })
    .WithAutomaticReconnect()
    .Build();
    
connection.On<string>("ReceiveMessage", Console.WriteLine);

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

await connection.StartAsync();
while (true) {
    var message = Console.ReadLine();
}