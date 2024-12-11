using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using chatApi.Managers;
using chatApi.Models;

namespace chatApi.Services;

public class WebsocketHandler
{
    private WebsocketManager _websocketManager;

    private CommandHandler _commandHandler;
    public WebsocketHandler(WebsocketManager websocketManager, CommandHandler commandHandler) 
    {
        _websocketManager = websocketManager;
        _commandHandler = commandHandler;
    }
    
    /// <summary>
    /// Handles the incoming Websocket Connection request.
    /// </summary>
    /// <param name="context">The HTTP Context.</param>
    /// <returns>A task that represents the operation.</returns>
    public async Task HandleWebSocketAsync(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            if (!context.Request.Headers.TryGetValue("userId", out var userId))
            {
                throw new WebSocketException("UserId header is missing.");
            }
            
            int id = int.Parse(userId!);
            
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

            await ProcessWebSocketAsync(id, webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }

    /// <summary>
    /// Handles a websocket connection.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="webSocket">The active websocket connection.</param>
    /// <returns>A task that represents the operation.</returns>
    public async Task ProcessWebSocketAsync(int userId, WebSocket webSocket)
    {
        _websocketManager.AddConnection(userId, webSocket);
        _websocketManager.SendMessage(userId, userId.ToString());
        
        var buffer = new byte[1024 * 4];
        
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                _websocketManager.RemoveConnection(userId);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "closing", CancellationToken.None);
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                MessageData messageData;
                
                try
                {
                    messageData = JsonSerializer.Deserialize<MessageData>(message)!;
                    _commandHandler.Handle(messageData);
                }
                catch (Exception e)
                {
                    _websocketManager.SendMessage(userId, e.Message);
                }
            }
        }
        _websocketManager.RemoveConnection(userId);
    }
}