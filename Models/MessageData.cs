using System.ComponentModel;

namespace chatApi.Models;

public class MessageData
{
    public String Command { get; set; }
    public int? RoomId { get; set; }

    public int? ReceiverId { get; set; }
    public int UserId { get; set; }
    public String? Message { get; set; }
}
    