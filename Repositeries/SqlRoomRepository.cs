using chatApi.Models;
using Microsoft.EntityFrameworkCore;

namespace chatApi.Repositeries;

public class SqlRoomRepository() : IRoomRepository
{
    private readonly AppDbContext _context;
    
    public SqlRoomRepository(AppDbContext context) : this()
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<Rooms?>> GetRooms()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Rooms?> GetRoomById(Guid id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task<Rooms?> InsertRoom(Rooms room)
    { 
        await _context.Rooms.AddAsync(room);
        return room;
    }

    public async Task<Rooms?> DeleteRoom(Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);
        _context.Rooms.Remove(room);

        return room;
    }

    public async Task<Rooms?> UpdateRoom(Guid id, Rooms room)
    {
        var roomToUpdate = await _context.Rooms.FindAsync(id);
        
        roomToUpdate!.Name = room.Name;
        roomToUpdate.Description = room.Description;
        
        return room;
    }

    public async Task Save()
    { 
        await _context.SaveChangesAsync();
    }
}