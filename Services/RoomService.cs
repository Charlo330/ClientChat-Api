using chatApi.Managers;

namespace chatApi.Services;

/// <summary>
/// Service of the rooms.
/// </summary>
public class RoomService
{
    /// <summary>
    /// Dictionary that contains the RoomManager object with his id.
    /// </summary>
    private Dictionary<int, RoomManager> _rooms;

    /// <summary>
    /// Function to add a Room to the dictionary.
    /// </summary>
    /// <param name="id">The id of the room.</param>
    /// <param name="manager">The RoomManager.</param>
    public void AddRoom(int id, RoomManager manager)
    {
        _rooms.Add(id, manager);
    }

    /// <summary>
    /// Function to remove a Room from the dictionary
    /// </summary>
    /// <param name="id">The id of the Room.</param>
    public void RemoveRoom(int id)
    {
        _rooms.Remove(id);
    }

    /// <summary>
    /// Function to send a message to a specific Room. 
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="roomId">The room id.</param>
    /// <param name="message">The message.</param>
    public void SendMessageToRoom(int userId, int roomId, string message)
    {
        _rooms[roomId].SendMessageToRoom(userId, message);
    }
}