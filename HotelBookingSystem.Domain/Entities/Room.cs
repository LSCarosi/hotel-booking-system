namespace HotelBookingSystem.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public int HotelId { get; private set; }
    public string RoomIdentifier { get; private set; }
    public int Capacity => SingleBeds + (DoubleBeds * 2); // Capacidade calculada automaticamente
    public int SingleBeds { get; private set; }
    public int DoubleBeds { get; private set; }
    public decimal PricePerNight { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public Hotel Hotel { get; private set; }
    public List<Reservation> Reservations { get; private set; } = new();

    private Room() { }

    public Room(int hotelId, string roomIdentifier, decimal pricePerNight, int singleBeds, int doubleBeds)
    {
        if (singleBeds < 0 || doubleBeds < 0)
            throw new ArgumentException("O número de camas não pode ser negativo.");

        HotelId = hotelId;
        RoomIdentifier = roomIdentifier;
        SingleBeds = singleBeds;
        DoubleBeds = doubleBeds;
        PricePerNight = pricePerNight;
        IsAvailable = true;
    }

    public void UpdateRoom(string? roomIdentifier, int? singleBeds, int? doubleBeds, decimal? pricePerNight)
    {
        if (!string.IsNullOrEmpty(roomIdentifier))
            RoomIdentifier = roomIdentifier;

        if (singleBeds.HasValue && singleBeds.Value >= 0)
            SingleBeds = singleBeds.Value;

        if (doubleBeds.HasValue && doubleBeds.Value >= 0)
            DoubleBeds = doubleBeds.Value;

        if (pricePerNight.HasValue && pricePerNight.Value > 0)
            PricePerNight = pricePerNight.Value;
    }

    public void MarkAsReserved()
    {
        IsAvailable = false;
    }

    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }
}
