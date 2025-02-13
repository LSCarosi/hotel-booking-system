using HotelBookingSystem.Application.DTO.Reservation;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;

        public ReservationController(IReservationRepository reservationRepository, IRoomRepository roomRepository, IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (reservation == null) return NotFound("Reserva não encontrada.");

            var reservationDTO = new ReservationDTO
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                RoomId = reservation.RoomId,
                CheckIn = reservation.CheckIn,
                CheckOut = reservation.CheckOut,
                Status = reservation.Status
            };
            return Ok(reservationDTO);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReservationsByUser(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return NotFound("Usuário não encontrado.");

            var reservations = await _reservationRepository.GetReservationByUserIdAsync(userId);
            var reservationDTOs = reservations.Select(r => new ReservationDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                RoomId = r.RoomId,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Status = r.Status
            }).ToList();

            if (reservationDTOs.Count == 0) return NoContent();

            return Ok(reservationDTOs);
        }

        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetReservationsByRoom(int roomId)
        {
            var room = await _roomRepository.GetRoomByIdAsync(roomId);
            if (room == null) return NotFound("Quarto não encontrado.");

            var reservations = await _reservationRepository.GetReservationByRoomIdAsync(roomId);
            var reservationDTOs = reservations.Select(r => new ReservationDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                RoomId = r.RoomId,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Status = r.Status
            }).ToList();

            if (reservationDTOs.Count == 0) return NoContent();

            return Ok(reservationDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDTO createReservationDTO)
        {
            var room = await _roomRepository.GetRoomByIdAsync(createReservationDTO.RoomId);
            if (room == null) return NotFound("Quarto não encontrado.");
            if (!room.IsAvailable) return BadRequest("O quarto já está reservado.");

            var reservation = new Reservation(createReservationDTO.UserId, room, createReservationDTO.RoomId, createReservationDTO.CheckIn, createReservationDTO.CheckOut);
            await _reservationRepository.AddReservationAsync(reservation);
            room.MarkAsReserved();
            await _roomRepository.UpdateRoomAsync(room);

            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, new ReservationDTO
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                RoomId = reservation.RoomId,
                CheckIn = reservation.CheckIn,
                CheckOut = reservation.CheckOut,
                Status = reservation.Status
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, UpdateReservationDTO updateReservationDTO)
        {
            var existingReservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (existingReservation == null) return NotFound("Reserva não encontrada.");

            existingReservation.Update(updateReservationDTO.CheckIn, updateReservationDTO.CheckOut);
            await _reservationRepository.UpdateReservationAsync(existingReservation);
            return Ok(new ReservationDTO
            {
                Id = existingReservation.Id,
                UserId = existingReservation.UserId,
                RoomId = existingReservation.RoomId,
                CheckIn = existingReservation.CheckIn,
                CheckOut = existingReservation.CheckOut,
                Status = existingReservation.Status
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (reservation == null) return NotFound("Reserva não encontrada.");

            var room = await _roomRepository.GetRoomByIdAsync(reservation.RoomId);
            if (room != null)
            {
                room.MarkAsAvailable();
                await _roomRepository.UpdateRoomAsync(room);
            }

            await _reservationRepository.DeleteReservationAsync(id);

            return Ok($"Reserva ID {id} deletado com sucesso!");

            //return NoContent();
        }
    }
}
