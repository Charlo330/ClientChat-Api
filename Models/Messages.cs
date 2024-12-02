namespace chatApi.Models;

public class Messages
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
    public DateTime Date { get; set; }

    public int UserId { get; set; }
    
    public int RoomId { get; set; }
}