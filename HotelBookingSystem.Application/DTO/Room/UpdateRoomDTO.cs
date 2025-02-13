﻿namespace HotelBookingSystem.Application.DTO.Room;

public class UpdateRoomDTO
{
    public string? RoomIdentifier { get; set; }
    public int? Capacity { get; set; }
    public int? SingleBeds { get; set; }
    public int? DoubleBeds { get; set; }
    public decimal? PricePerNight { get; set; }
}
