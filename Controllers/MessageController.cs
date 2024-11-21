using Microsoft.AspNetCore.Mvc;

namespace chatApi.Controllers;

[ApiController]
[Route("/api/messages")]
public class MessageController : ControllerBase
{
    [HttpGet("{id}")]
    public String GetMessage(int id)
    {
        return "Test12";
    }
    
}