using chatApi.Managers;
using chatApi.Models;
using chatApi.Repositeries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace chatApi.Services;

/// <summary>
/// Service of the rooms.
/// </summary>
public class RoomService
{
    /// <summary>
    /// Dictionary that contains the RoomManager object with his id.
    /// </summary>
    private Dictionary<Guid, RoomManager> _rooms;

    /// <summary>
    /// Instance of WebsocketManager used to handle WebSocket connections
    /// and facilitate communication between clients.
    /// </summary>
    private WebsocketManager _websocketManager;

    /// <summary>
    /// Repository used for accessing and managing room data in the database.
    /// </summary>
    private SqlRoomRepository _roomRepository;

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="websocketManager">The websocket manager.</param>
    /// <param name="sqlRoomRepository">The Room repository.</param>
    public RoomService(WebsocketManager websocketManager, SqlRoomRepository sqlRoomRepository)
    {
        _websocketManager = websocketManager;
        _roomRepository = sqlRoomRepository;
        _rooms = new Dictionary<Guid, RoomManager>();
    }

    /// <summary>
    /// Retrieves all rooms from the repository.
    /// </summary>
    /// <returns>A collection of rooms or null if no rooms are found.</returns>
    public async Task<IEnumerable<Rooms?>> GetAllRooms()
    {
        return await _roomRepository.GetRooms();
    }

    /// <summary>
    /// Function to add a Room to the dictionary.
    /// </summary>
    /// <param name="id">The id of the room.</param>
    public async Task<Rooms> CreateRoom(string name, string description, Guid userId)
    {
        var room = await _roomRepository.InsertRoom(new Rooms(name, description, userId));
        await _roomRepository.Save();
        if (room != null)
            _rooms.Add(room.Id, new RoomManager(_websocketManager));
                
        return room!;
    }

    /// <summary>
    /// Function to remove a Room from the dictionary
    /// </summary>
    /// <param name="id">The id of the Room.</param>
    public async Task<Rooms?> RemoveRoom(Guid id)
    {
        var room = await _roomRepository.DeleteRoom(id);
        await _roomRepository.Save();
        
        if (room != null)
            _rooms.Remove(id);
        return room;
    }

    /// <summary>
    /// Adds a user to a specified room.
    /// </summary>
    /// <param name="roomId">The unique identifier of the room.</param>
    /// <param name="userId">The unique identifier of the user to be added.</param>
    public void AddUserToRoom(Guid roomId, Guid userId)
    {
        if (_rooms.TryGetValue(roomId, out RoomManager? manager))
            manager.AddUserToRoom(userId);
    }

    /// <summary>
    /// Function to send a message to a specific Room. 
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="roomId">The room id.</param>
    /// <param name="message">The message.</param>
    public void SendMessageToRoom(Guid userId, Guid roomId, string message)
    {
        _rooms[roomId].SendMessageToRoom(userId, message);
    }
}