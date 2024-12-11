using chatApi.Models;

namespace chatApi.Repositeries;

public interface IRoomRepository
{
    Task<IEnumerable<Rooms?>> GetRooms();
    
    Task<Rooms?> GetRoomById(Guid id);
    
    Task<Rooms?> InsertRoom(Rooms room);
    
    Task<Rooms?> DeleteRoom(Guid id);
    
    Task<Rooms?> UpdateRoom(Guid id, Rooms room);
    
    Task Save();
}