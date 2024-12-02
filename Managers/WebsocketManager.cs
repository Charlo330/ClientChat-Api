using System.Net.WebSockets;
using System.Text;

namespace chatApi.Managers;

/// <summary>
/// Class that Manages the websocketConnection.
/// </summary>
public class WebsocketManager
{
    /// <summary>
    /// Dictionary that contains the websocket connections with the user id.
    /// </summary>
    private Dictionary<int, WebSocket> _connections = new Dictionary<int, WebSocket>();

    /// <summary>
    /// Function that adds a connection to the connection's dictionary.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="socket">the websocket connection.</param>
    public void AddConnection(int id, WebSocket socket)
    {
        _connections.Add(id, socket);
    }

    /// <summary>
    /// Function that removes a connection from the dictionary.
    /// </summary>
    /// <param name="id">The user id.</param>
    public void RemoveConnection(int id)
    {
        _connections.Remove(id);
    }

    /// <summary>
    /// Function that sends a message to the websocket.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="message">The message.</param>
    public void SendMessage(int id, string message)
    {
        byte[] messageBytes = Encoding.Default.GetBytes(message);
        
        _connections[id].SendAsync(messageBytes, WebSocketMessageType.Text, true, CancellationToken.None);
    }
}