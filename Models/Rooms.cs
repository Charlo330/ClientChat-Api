namespace chatApi.Models;

public class Rooms
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string creator { get; set; }
    
    public string description { get; set; }
}