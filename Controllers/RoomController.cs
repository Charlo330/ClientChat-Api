using chatApi.Managers;
using chatApi.Models;
using chatApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatApi.Controllers;

[ApiController]
[Route("/api/rooms")]
public class RoomController : ControllerBase
{
    /// <summary>
    /// Provides access to the RoomService instance.
    /// </summary>
    private readonly RoomService _roomService;

    /// <summary>
    /// Controller responsible for managing operations related to chat rooms.
    /// </summary>
    public RoomController(RoomService roomService)
    {
        _roomService = roomService;
    }

    /// <summary>
    /// Retrieves all available chat rooms.
    /// </summary>
    /// <returns>Returns an HTTP response containing a list of chat rooms if successful, or an error message if an exception occurs.</returns>
    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        try
        {
            var roomRetrieved = _roomService.GetAllRooms();
            return Ok(new { Message = "", Data = roomRetrieved });
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Creates a new chat room.
    /// </summary>
    /// <param name="room">Represents the details of the room to be created, including its name, description, and creator ID.</param>
    /// <returns>Returns an HTTP response containing the details of the created room if successful, or an error message if an exception occurs.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomDto room)
    {
        try
        {
            var roomRetrieved = await _roomService.CreateRoom(room.Name!, room.Description!, room.CreatorId!.Value);
            return Ok(new { Message = "Room created!", Data = roomRetrieved });
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Deletes an existing chat room.
    /// </summary>
    /// <param name="room">Represents the details of the room to be deleted, including its ID.</param>
    /// <returns>Returns an HTTP response containing a success message and the deleted room details if successful, or an error message if an exception occurs.</returns>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] RoomDto room)
    {
        try
        {
            if (!room.Id.HasValue)
            {
                return BadRequest("Room does not exist");
            }

            var roomRetrieved = await _roomService.RemoveRoom(room.Id.Value);
            return Ok(new { Message = "Room deleted!", Data = roomRetrieved });
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Allows a user to join a specified chat room.
    /// </summary>
    /// <param name="userId">Represents the unique identifier of the user attempting to join the chat room.</param>
    /// <param name="roomId">Represents the unique identifier of the chat room the user wants to join.</param>
    /// <returns>Returns an HTTP response containing a success message if the user is successfully added to the room, or an error message if an exception occurs.</returns>
    [HttpGet("join/{roomId}")]
    public async Task<IActionResult> JoinRoom([FromBody] UserId userId, Guid roomId)
    {
        try
        {
            _roomService.AddUserToRoom(roomId, userId.userId);
            return Ok(new { Message = "Room joined!", Data = Empty});
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}

/// <summary>
/// Data Transfer Object representing the details of a chat room.
/// </summary>
public class RoomDto
{
    public Guid? Id { get; set; }
    
    public string? Name { get; set; }
    
    public Guid? CreatorId { get; set; }
    
    public string? Description { get; set; }
}

/// <summary>
/// Data Transfer Object representing the unique identifier of a user.
/// </summary>
public class UserId
{
    public Guid userId { get; set; }
}