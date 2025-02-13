using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Enums;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _context;

    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetReservationByIdAsync(int id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task<List<Reservation>> GetAllReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<List<Reservation>> GetReservationByUserIdAsync(int userId)
    {
        return await _context.Reservations.Where(r => r.UserId == userId).ToListAsync();
    }

    public async Task<List<Reservation>> GetReservationByRoomIdAsync(int roomId)
    {
        return await _context.Reservations.Where(r => r.RoomId == roomId).ToListAsync();
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReservationAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Reservation>> GetExpiredReservationsAsync()
    {
        return await _context.Reservations
            .Where(r => r.CheckOut.AddDays(1) <= DateTime.UtcNow && r.Status == ReservationStatus.Confirmed)
            .ToListAsync();
    }

}
