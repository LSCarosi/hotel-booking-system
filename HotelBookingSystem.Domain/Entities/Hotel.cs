namespace HotelBookingSystem.Domain.Entities;

public class Hotel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public decimal PricePerNight { get; private set; }
    public int OwnerId { get; private set; }

    private Hotel() { }

    public Hotel(string name, string location, decimal pricePerNight, int ownerId)
    {
        Name = name;
        Location = location;
        PricePerNight = pricePerNight;
        OwnerId = ownerId;
    }
}
