using chatApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatApi.Controllers;

[ApiController]
[Route("/api/messages")]
public class MessageController : ControllerBase
{
    RoomService roomService;
    
    [HttpGet("{id}")]
    public String GetMessage(int id)
    {
        return "Test12";
    }

}