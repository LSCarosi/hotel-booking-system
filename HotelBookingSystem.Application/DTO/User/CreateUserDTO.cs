using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTO.User;

public class CreateUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}
