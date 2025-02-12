using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
}
