using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IHotelRepository
{
    Task<Hotel?> GetHotelByIdAsync(int id);
    Task<List<Hotel>> GetAllHotelsAsync();
    Task<List<Hotel>> GetHotelByOwnerIdAsync(int ownerId);
    Task AddHotelAsync(Hotel hotel);
    Task UpdateHotelAsync(Hotel hotel);
    Task DeleteHotelAsync(int id);
}
