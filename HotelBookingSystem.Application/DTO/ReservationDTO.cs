using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTO;

public class ReservationDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public ReservationStatus Status { get; set; }
}
