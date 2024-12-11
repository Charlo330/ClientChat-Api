using System.ComponentModel;

namespace chatApi.Models;

public class MessageData
{
    public String Command { get; set; }
    public Guid? RoomId { get; set; }

    public Guid? ReceiverId { get; set; }
    public Guid UserId { get; set; }
    public String? Message { get; set; }
}
    