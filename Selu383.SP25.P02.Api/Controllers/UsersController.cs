using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Data;
using Selu383.SP25.P02.Api.Features;
using Selu383.SP25.P02.Api.Features.DTOs;
using static Selu383.SP25.P02.Api.Controllers.UsersController;


namespace Selu383.SP25.P02.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
            private readonly UserManager<User> _userManager;

            public UsersController(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            [HttpPost]
            public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new User
                {
                    UserName = createUserDto.UserName
                };

                var result = await _userManager.CreateAsync(user, createUserDto.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors); // Return errors if creation fails

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new UserDto { Id = user.Id, UserName = user.UserName });
            }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            // Load the user with their associated roles using eager loading
            var user = await _userManager.Users
                .Include(u => u.Roles)               // Eagerly load the UserRoles collection
                .ThenInclude(ur => ur.Role)               // Eagerly load the Role associated with each UserRole
                .FirstOrDefaultAsync(u => u.Id == id);    // Find the user by ID

            if (user == null)
                return NotFound();

            // Map the User and Roles to a UserDto
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = user.Roles.Select(ur => ur.Role.Name).ToList()  // Map Role Names to List
            };

            return Ok(userDto);
        }
    }


    }
