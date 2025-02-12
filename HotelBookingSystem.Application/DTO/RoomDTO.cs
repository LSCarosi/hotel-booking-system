namespace HotelBookingSystem.Application.DTO;

public class RoomDTO
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string RoomIdentifier { get; set; }
    public int Capacity { get; set; }
    public int SingleBeds { get; set; }
    public int DoubleBeds { get; set; }
    public decimal PricePerNight { get; set; }
}
