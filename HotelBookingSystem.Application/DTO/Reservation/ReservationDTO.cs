using HotelBookingSystem.Domain.Enums;

namespace HotelBookingSystem.Application.DTO.Reservation;

public class ReservationDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut{ get; set; }
    public ReservationStatus Status { get; set; }
}
