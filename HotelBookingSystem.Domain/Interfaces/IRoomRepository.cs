using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces;

public interface IRoomRepository
{
    Task<Room?> GetRoomByIdAsync(int id);
    Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId);
    Task<List<Room>> GetAvailableRoomsAsync(int hotelId, DateTime checkIn, DateTime checkOut);
    Task AddRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task DeleteRoomAsync(int id);
}
