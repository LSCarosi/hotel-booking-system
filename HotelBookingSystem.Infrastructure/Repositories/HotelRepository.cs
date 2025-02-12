using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace HotelBookingSystem.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly ApplicationDbContext _context;

    public HotelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Hotel?> GetHotelByIdAsync(int id)
    {
        return await _context.Hotels.FindAsync(id);
    }

    public async Task<List<Hotel>> GetAllHotelsAsync()
    {
        return await _context.Hotels.ToListAsync();
    }

    public async Task<List<Hotel>> GetHotelsByOwnerIdAsync(int ownerId)
    {
        return await _context.Hotels.Where(h => h.OwnerId == ownerId).ToListAsync();
    }

    public async Task AddHotelAsync(Hotel hotel)
    {
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateHotelAsync(Hotel hotel)
    {
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteHotelAsync(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
    }
}
