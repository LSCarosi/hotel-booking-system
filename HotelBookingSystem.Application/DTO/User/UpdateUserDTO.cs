using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTO.User;

public class UpdateUserDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public UserRole? Role { get; set; }
}
