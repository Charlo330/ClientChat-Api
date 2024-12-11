using chatApi.Managers;
using chatApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatApi.Controllers;

[ApiController]
[Route("/api/rooms")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;

    public RoomController(RoomService roomService)
    {
        _roomService = roomService;
    }
    
    [HttpPost("create")]
    public String Create([FromBody] RoomId roomId)
    {
        _roomService.CreateRoom(roomId.roomId);
        return "Success";
    }
    
    [HttpGet("join/{roomId}")]
    public String JoinRoom([FromBody] UserId userId, int roomId)
    {
        _roomService.AddUserToRoom(roomId, userId.userId);
        return "Success";
    }
}

public class RoomId
{
    public int roomId { get; set; }
}

public class UserId
{
    public int userId { get; set; }
}