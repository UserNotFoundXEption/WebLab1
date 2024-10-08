using Microsoft.AspNetCore.WebSockets;
using WebLab1;
using Fleck;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddWebSockets(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(120);
});

var socketServer = new WebSocketServer("ws://127.0.0.1:2137/ws");
socketServer.Start(connection =>
{
    connection.OnOpen = () =>
    {
        Console.WriteLine("Websocket is working");
    };

    connection.OnMessage = message =>
    {
        try
        {
            var jsonMessage = JObject.Parse(message);
            string type = jsonMessage["type"]?.ToString();
            int number = jsonMessage["number"]?.ToObject<int>() ?? 0;

            string response = "";

            if (type == "fibonacci")
            {
                response = Calculations.Fibonacci(number);
            }
            else if (type == "factorial")
            {
                response = Calculations.Factorial(number);
            }
            else
            {
                response = "Unsupported type";
            }

            Console.WriteLine("response: " + response);
            connection.Send(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while processing message: " + ex.Message);
        }
    };

    connection.OnClose = () =>
    {
        Console.WriteLine("Websocket connection closed");
    };
});

var app = builder.Build();

app.UseStaticFiles();
app.UseWebSockets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();