namespace chatApi.Models;

public class Rooms
{
    public Rooms(string name, string description, Guid creatorId)
    {
        Name = name;
        CreatorId = creatorId;
        Description = description;
    }
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Guid CreatorId { get; set; }
    
    public string Description { get; set; }
}