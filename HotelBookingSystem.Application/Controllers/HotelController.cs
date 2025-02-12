using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelRepository _hotelRepository;

    public HotelController(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHotelById(int id)
    {
        var hotel = await _hotelRepository.GetHotelByIdAsync(id);
        if (hotel == null) return NotFound("Hotel não encontrado.");
        return Ok(hotel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHotels()
    {
        var hotels = await _hotelRepository.GetAllHotelsAsync();
        return Ok(hotels);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotel(Hotel hotel)
    {
        await _hotelRepository.AddHotelAsync(hotel);
        return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(int id, Hotel updatedHotel)
    {
        var existingHotel = await _hotelRepository.GetHotelByIdAsync(id);
        if (existingHotel == null) return NotFound("Hotel não encontrado.");

        await _hotelRepository.UpdateHotelAsync(updatedHotel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        await _hotelRepository.DeleteHotelAsync(id);
        return NoContent();
    }
}
