using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Domain.Entities;

public class Reservation
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int HotelId { get; private set; }
    public DateTime CheckIn { get; private set; }
    public DateTime CheckOut { get; private set; }
    public ReservationStatus Status { get; private set; }

    private Reservation() { }

    public Reservation(int userId, int hotelId, DateTime checkIn, DateTime checkOut)
    {
        if (checkOut <= checkIn)
            throw new ArgumentException("CheckOut deve ser depois do CheckIn.");

        UserId = userId;
        HotelId = hotelId;
        CheckIn = checkIn;
        CheckOut = checkOut;
        Status = ReservationStatus.Pending;
    }
}
