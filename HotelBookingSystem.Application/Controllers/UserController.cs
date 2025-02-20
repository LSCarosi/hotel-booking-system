using HotelBookingSystem.Application.DTO.User;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBookingSystem.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) return NotFound("Usuário não encontrado.");

        var userDto = new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };

        return Ok(userDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();

        var userDtos = users.Select(user => new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        }).ToList();

        return Ok(userDtos);
    }

    [AllowAnonymous] 
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
    {
        var user = new User(createUserDTO.Name, createUserDTO.Email, createUserDTO.Password, createUserDTO.Role);
        await _userRepository.AddUserAsync(user);

        var userDTO = new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDTO);
    }

    [HttpPut("update-password/{Id}")]
    public async Task<IActionResult> UpdatePassword(int Id, UpdatePasswordDTO passwordDto)
    {
        var user = await _userRepository.GetUserByIdAsync(Id);
        if (user == null) return NotFound("Usuário não encontrado.");

        if (user.Password != passwordDto.CurrentPassword)
            return BadRequest("Senha atual incorreta.");

        user.UpdatePassword(passwordDto.NewPassword);
        await _userRepository.UpdateUserAsync(user);

        return Ok($"Senha do Usuário ID {Id} alterada com sucesso!");
        //return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO updatedUser)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(id);
        if (existingUser == null) return NotFound("Usuário não encontrado.");

        existingUser.UpdateUser(updatedUser.Name, updatedUser.Email, updatedUser.Role);
        await _userRepository.UpdateUserAsync(existingUser);

        return Ok(new UserDTO
        {
            Id = existingUser.Id,
            Name = existingUser.Name,
            Email = existingUser.Email,
            Role = existingUser.Role
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(id);
        if (existingUser == null) return NotFound("Usuário não encontrado.");

        await _userRepository.DeleteUserAsync(id);
        return Ok($"Usuário ID {id} deletado com sucesso!");
        //return NoContent();
    }
}
