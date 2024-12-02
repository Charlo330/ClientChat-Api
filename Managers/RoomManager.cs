using Microsoft.AspNetCore.Connections;

namespace chatApi.Managers;

/// <summary>
/// Class that manage a room.
/// </summary>
public class RoomManager
{
    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="websocketManager">The websocketManager (Injected)</param>
    public RoomManager(WebsocketManager websocketManager)
    {
        _websocketManager = websocketManager;
        _usersInRoom = new List<int>();
    }
    
    /// <summary>
    /// The WebsocketManager class.
    /// </summary>
    private WebsocketManager _websocketManager;

    /// <summary>
    /// The list of user connected in the room.
    /// </summary>
    private List<int> _usersInRoom;

    /// <summary>
    /// Function to add a user to the room.
    /// </summary>
    /// <param name="userId">The user id.</param>
    public void AddUserToRoom(int userId)
    {
        _usersInRoom.Add(userId);
    }

    /// <summary>
    /// Function to remove a user from the room.
    /// </summary>
    /// <param name="userId"></param>
    public void RemoveUserFromRoom(int userId)
    {
        _usersInRoom.Remove(userId);
    }

    /// <summary>
    /// Function to send a message to every user in the room.
    /// </summary>
    /// <param name="userId">The user id of the connected person.</param>
    /// <param name="message">The message.</param>
    public void SendMessageToRoom(int userId, string message)
    {
        foreach (var user in _usersInRoom) {
            if (userId != user)
                _websocketManager.SendMessage(user, message);
        }
    }
    
}