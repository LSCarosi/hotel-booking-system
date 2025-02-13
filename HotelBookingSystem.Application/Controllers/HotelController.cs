using HotelBookingSystem.Application.DTO.Hotel;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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

        return Ok(new HotelDTO
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Location = hotel.Location,
            OwnerId = hotel.OwnerId
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHotels()
    {
        var hotels = await _hotelRepository.GetAllHotelsAsync();
        var hotelDTOs = hotels.Select(h => new HotelDTO
        {
            Id = h.Id,
            Name = h.Name,
            Location = h.Location,
            OwnerId = h.OwnerId
        }).ToList();

        return Ok(hotelDTOs);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotel(CreateHotelDTO createHotelDTO)
    {
        var hotel = new Hotel(createHotelDTO.Name, createHotelDTO.Location, createHotelDTO.OwnerId);
        await _hotelRepository.AddHotelAsync(hotel);

        return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, new HotelDTO
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Location = hotel.Location,
            OwnerId = hotel.OwnerId
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(int id, UpdateHotelDTO updateHotelDTO)
    {
        var existingHotel = await _hotelRepository.GetHotelByIdAsync(id);
        if (existingHotel == null) return NotFound("Hotel não encontrado.");

        existingHotel.UpdateHotel(updateHotelDTO.Name, updateHotelDTO.Location);
        await _hotelRepository.UpdateHotelAsync(existingHotel);

        return Ok(new HotelDTO
        {
            Id = existingHotel.Id,
            Name = existingHotel.Name,
            Location = existingHotel.Location,
            OwnerId = existingHotel.OwnerId
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var existingHotel = await _hotelRepository.GetHotelByIdAsync(id);
        if (existingHotel == null) return NotFound("Hotel não encontrado.");

        await _hotelRepository.DeleteHotelAsync(id);
        return NoContent();
    }
}
