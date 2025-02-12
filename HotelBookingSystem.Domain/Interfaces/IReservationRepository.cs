using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IReservationRepository
{
    Task<Reservation?> GetReservationByIdAsync(int id);
    Task<List<Reservation>> GetAllReservationsAsync();
    Task<List<Reservation>> GetReservationByUserIdAsync(int userId);
    Task<List<Reservation>> GetReservationByHotelIdAsync(int hotelId);
    Task AddReservationAsync(Reservation reservation);
    Task UpdateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(int id);
}
