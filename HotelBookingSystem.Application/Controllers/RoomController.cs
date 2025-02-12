using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;

    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var room = await _roomRepository.GetRoomByIdAsync(id);
        if (room == null) return NotFound("Quarto não encontrado.");
        return Ok(room);
    }

    [HttpGet("hotel/{hotelId}")]
    public async Task<IActionResult> GetRoomsByHotel(int hotelId)
    {
        var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
        return Ok(rooms);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom(Room room)
    {
        await _roomRepository.AddRoomAsync(room);
        return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, Room updatedRoom)
    {
        var existingRoom = await _roomRepository.GetRoomByIdAsync(id);
        if (existingRoom == null) return NotFound("Quarto não encontrado.");

        await _roomRepository.UpdateRoomAsync(updatedRoom);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        await _roomRepository.DeleteRoomAsync(id);
        return NoContent();
    }
}
