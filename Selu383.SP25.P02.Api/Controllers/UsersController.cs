using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Data;
using Selu383.SP25.P02.Api.Features;
using Selu383.SP25.P02.Api.Features.DTOs;


namespace Selu383.SP25.P02.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;
            private readonly DataContext _context;

        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost]
        [Authorize]
 // Ensures only authenticated users can create new users
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

            // Assign roles if provided
            if (createUserDto.Roles != null && createUserDto.Roles.Any())
            {
                foreach (var roleName in createUserDto.Roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = user.Id,
                            RoleId = role.Id
                        };

                        _context.UserRoles.Add(userRole);
                    }
                    else
                    {
                        return BadRequest($"Role '{roleName}' does not exist.");
                    }
                }

                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = createUserDto.Roles
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = user.Roles.Select(ur => ur.Role.Name).ToList()
            };

            return Ok(userDto);
        }
    }


    }
