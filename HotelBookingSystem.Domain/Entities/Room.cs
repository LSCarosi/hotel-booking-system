namespace HotelBookingSystem.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public int HotelId { get; private set; }
    public string RoomIdentifier { get; private set; }
    public int Capacity { get; private set; }
    public int SingleBeds { get; private set; } 
    public int DoubleBeds { get; private set; } 
    public decimal PricePerNight { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public Hotel Hotel { get; private set; }
    public List<Reservation> Reservations { get; private set; } = new();

    private Room() { }

    public Room(int hotelId, string roomIdentifier, int capacity, decimal pricePerNight, int singleBeds, int doubleBeds)
    {
        if (singleBeds < 0 || doubleBeds < 0)
            throw new ArgumentException("O número de camas não pode ser negativo.");

        if (singleBeds + (doubleBeds * 2) > capacity)
            throw new ArgumentException("A quantidade de camas não pode ser maior que a capacidade do quarto.");

        HotelId = hotelId;
        RoomIdentifier = roomIdentifier;
        Capacity = capacity;
        SingleBeds = singleBeds;
        DoubleBeds = doubleBeds;
        PricePerNight = pricePerNight;
        IsAvailable = true;
    }

    public void MarkAsReserved()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Este quarto já está reservado.");
        IsAvailable = false;
    }

    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }
}
