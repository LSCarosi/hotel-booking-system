namespace HotelBookingSystem.Domain.Entities;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public int OwnerId { get; set; }  

    public List<Room> Rooms { get; private set; } = new();  

    private Hotel() { }

    public Hotel(string name, string location, int ownerId)
    {
        Name = name;
        Location = location;
        OwnerId = ownerId;
    }

    public void AddRoom(Room room)
    {
        Rooms.Add(room);
    }
}

