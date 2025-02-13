using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }

    private User() { }

    public User(string name, string email, string password, UserRole role)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    public void UpdateUser(string? name, string? email, UserRole? role)
    {
        if (!string.IsNullOrEmpty(name)) Name = name;
        if (!string.IsNullOrEmpty(email)) Email = email;
        if (role.HasValue) Role = role.Value;
    }

    public void UpdatePassword(string password)
    {
        if (!string.IsNullOrEmpty(password))
            Password = password;
    }
}
