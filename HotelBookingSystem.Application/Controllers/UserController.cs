using HotelBookingSystem.Application.DTO.User;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBookingSystem.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [Authorize] 
    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO passwordDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized("Usuário não autenticado.");

        var existingUser = await _userRepository.GetUserByIdAsync(int.Parse(userId));
        if (existingUser == null) return NotFound("Usuário não encontrado.");

        if (existingUser.Password != passwordDto.CurrentPassword)
            return BadRequest("Senha atual incorreta.");

        existingUser.UpdatePassword(passwordDto.NewPassword);
        await _userRepository.UpdateUserAsync(existingUser);

        return NoContent();
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
