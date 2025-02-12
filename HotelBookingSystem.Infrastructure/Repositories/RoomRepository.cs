using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Room?> GetRoomByIdAsync(int id)
    {
        return await _context.Rooms.Include(r => r.Hotel).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId)
    {
        return await _context.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
    }

    public async Task<List<Room>> GetAvailableRoomsAsync(int hotelId, DateTime checkIn, DateTime checkOut)
    {
        return await _context.Rooms
            .Where(r => r.HotelId == hotelId)
            .Where(r => !r.Reservations.Any(res =>
                (checkIn < res.CheckOut && checkOut > res.CheckIn)
            ))
            .ToListAsync();
    }

    public async Task AddRoomAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRoomAsync(Room room)
    {
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRoomAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room != null)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}
