using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationController(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservationById(int id)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound("Reserva não encontrada.");
        return Ok(reservation);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReservationsByUser(int userId)
    {
        var reservations = await _reservationRepository.GetReservationByUserIdAsync(userId);
        return Ok(reservations);
    }

    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> GetReservationsByRoom(int roomId)
    {
        var reservations = await _reservationRepository.GetReservationByRoomIdAsync(roomId);
        return Ok(reservations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(Reservation reservation)
    {
        await _reservationRepository.AddReservationAsync(reservation);
        return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservation(int id, Reservation updatedReservation)
    {
        var existingReservation = await _reservationRepository.GetReservationByIdAsync(id);
        if (existingReservation == null) return NotFound("Reserva não encontrada.");

        await _reservationRepository.UpdateReservationAsync(updatedReservation);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        await _reservationRepository.DeleteReservationAsync(id);
        return NoContent();
    }
}
