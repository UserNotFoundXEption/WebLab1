namespace WebLab1
{
    using System.Net.WebSockets;
    using System.Text;
    using System.Text.Json;

    public static class WebSocketHandler
    {
        public static async Task HandleWebSocketAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var data = JsonSerializer.Deserialize<ClientMessage>(message);

                    var responseMessage = HandleMessage(data);
                    var responseBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(responseMessage));

                    await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                }
            }
        }

        private static ServerMessage HandleMessage(ClientMessage data)
        {
            if (data.Type == "factorial")
            {
                return new ServerMessage { Result = Calculations.Factorial(data.Number) };
            }
            else if (data.Type == "fibonacci")
            {
                return new ServerMessage { Result = Calculations.Fibonacci(data.Number) };
            }

            return new ServerMessage { Error = "Unknown calculation type." };
        }
    }

    public class ClientMessage
    {
        public string Type { get; set; }
        public int Number { get; set; }
    }

    public class ServerMessage
    {
        public string Result { get; set; }
        public string Error { get; set; }
    }

}
