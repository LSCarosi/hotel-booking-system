using HotelBookingSystem.Application.DTO.Room;
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
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;

        public RoomController(IRoomRepository roomRepository, IHotelRepository hotelRepository)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room == null) return NotFound("Quarto não encontrado.");

            var roomDTO = new RoomDTO
            {
                Id = room.Id,
                HotelId = room.HotelId,
                RoomIdentifier = room.RoomIdentifier,
                Capacity = room.Capacity,
                SingleBeds = room.SingleBeds,
                DoubleBeds = room.DoubleBeds,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable
            };
            return Ok(roomDTO);
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotel(int hotelId)
        {
            var existingHotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            if (existingHotel == null) return NotFound("Hotel não encontrado.");

            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
            var roomDTOs = rooms.Select(room => new RoomDTO
            {
                Id = room.Id,
                HotelId = room.HotelId,
                RoomIdentifier = room.RoomIdentifier,
                Capacity = room.Capacity,
                SingleBeds = room.SingleBeds,
                DoubleBeds = room.DoubleBeds,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable
            }).ToList();
            return Ok(roomDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomDTO createRoomDTO)
        {
            var existingHotel = await _hotelRepository.GetHotelByIdAsync(createRoomDTO.HotelId);
            if (existingHotel == null) return NotFound("Hotel não encontrado.");

            var room = new Room(createRoomDTO.HotelId, createRoomDTO.RoomIdentifier, createRoomDTO.PricePerNight, createRoomDTO.SingleBeds, createRoomDTO.DoubleBeds);
            await _roomRepository.AddRoomAsync(room);
            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, new RoomDTO
            {
                Id = room.Id,
                HotelId = room.HotelId,
                RoomIdentifier = room.RoomIdentifier,
                Capacity = room.Capacity,
                SingleBeds = room.SingleBeds,
                DoubleBeds = room.DoubleBeds,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, UpdateRoomDTO updateRoomDTO)
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(id);
            if (existingRoom == null) return NotFound("Quarto não encontrado.");

            existingRoom.UpdateRoom(updateRoomDTO.RoomIdentifier, updateRoomDTO.SingleBeds, updateRoomDTO.DoubleBeds, updateRoomDTO.PricePerNight);
            await _roomRepository.UpdateRoomAsync(existingRoom);
            return Ok(new RoomDTO
            {
                Id = existingRoom.Id,
                HotelId = existingRoom.HotelId,
                RoomIdentifier = existingRoom.RoomIdentifier,
                Capacity = existingRoom.Capacity,
                SingleBeds = existingRoom.SingleBeds,
                DoubleBeds = existingRoom.DoubleBeds,
                PricePerNight = existingRoom.PricePerNight,
                IsAvailable = existingRoom.IsAvailable
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(id);
            if (existingRoom == null) return NotFound("Quarto não encontrado.");

            await _roomRepository.DeleteRoomAsync(id);
            return Ok($"Quarto ID {id} deletado com sucesso!");
            //return NoContent();
        }
    }
}
