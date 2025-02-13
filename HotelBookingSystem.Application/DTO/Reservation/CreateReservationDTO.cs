namespace HotelBookingSystem.Application.DTO.Reservation;

public class CreateReservationDTO
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
