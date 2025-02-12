using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; private set; } 
    public int RoomId { get; private set; } 
    public DateTime CheckIn { get; private set; }
    public DateTime CheckOut { get; private set; }
    public ReservationStatus Status { get; private set; }

    public User User { get; private set; }
    public Room Room { get; private set; }  

    private Reservation() { }

    public Reservation(int userId, int roomId, DateTime checkIn, DateTime checkOut)
    {
        if (checkOut <= checkIn)
            throw new ArgumentException("CheckOut deve ser depois do CheckIn.");

        UserId = userId;
        RoomId = roomId;
        CheckIn = checkIn;
        CheckOut = checkOut;
        Status = ReservationStatus.Pending;
    }
}

