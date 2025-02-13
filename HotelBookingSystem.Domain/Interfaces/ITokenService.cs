using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
