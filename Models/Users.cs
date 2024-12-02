namespace chatApi.Models;

public class Users
{
    public Guid Id { get; set; }
    
    public string userName { get; set; }
    
    public List<Rooms> roomJoin  { get; set; }
}