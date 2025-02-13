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

    public Reservation(int userId, Room room, int roomId, DateTime checkIn, DateTime checkOut)
    {
        if (checkOut <= checkIn)
            throw new ArgumentException("CheckOut deve ser depois do CheckIn.");

        if (!room.IsAvailable)
            throw new InvalidOperationException("Este quarto já está reservado.");

        UserId = userId;
        RoomId = roomId;
        Room = room;
        CheckIn = checkIn;
        CheckOut = checkOut;
        room.MarkAsReserved();
        Status = ReservationStatus.Confirmed;
    }

    public void Update(DateTime? checkIn, DateTime? checkOut)
    {
        if (checkIn.HasValue) CheckIn = checkIn.Value;
        if (checkOut.HasValue) CheckOut = checkOut.Value;
    }

    public void Cancel()
    {
        if (Status == ReservationStatus.Canceled)
            throw new InvalidOperationException("Esta reserva já foi cancelada.");

        Status = ReservationStatus.Canceled;
        Room.MarkAsAvailable(); 
    }
}

