namespace HotelBookingSystem.Application.DTO.Room;

public class CreateRoomDTO
{
    public int HotelId { get; set; }
    public string RoomIdentifier { get; set; }
    public int SingleBeds { get; set; }
    public int DoubleBeds { get; set; }
    public decimal PricePerNight { get; set; }
}
