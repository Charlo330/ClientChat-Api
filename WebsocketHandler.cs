using System.Net.WebSockets;
using System.Text;

namespace chatApi;

public class WebsocketHandler
{
    /// <summary>
    /// Handles the incoming Websocket Connection request.
    /// </summary>
    /// <param name="context">The HTTP Context.</param>
    /// <returns>A task that represents the operation.</returns>
    public async Task HandleWebSocketAsync(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

            await ProcessWebSocketAsync(webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }

    /// <summary>
    /// Handles a websocket connection.
    /// </summary>
    /// <param name="webSocket">The active websocket connection.</param>
    /// <returns>A task that represents the operation.</returns>
    public async Task ProcessWebSocketAsync(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "closing", CancellationToken.None);
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var responseMessage = Encoding.UTF8.GetBytes(message);
                await webSocket.SendAsync(new ArraySegment<byte>(responseMessage), result.MessageType, result.EndOfMessage, CancellationToken.None);
            }
        }
    }
}