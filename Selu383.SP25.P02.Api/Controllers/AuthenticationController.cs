using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Selu383.SP25.P02.Api.Features;
using Selu383.SP25.P02.Api.Features.DTOs;

namespace Selu383.SP25.P02.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        RoleManager<Role> roleManager
    ) : Controller
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<Role> _roleManager = roleManager;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Invalid login details");

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return BadRequest("Invalid username or password");

            var result = await _signInManager.PasswordSignInAsync(
                user,
                loginDto.Password,
                false,
                false
            );
            if (!result.Succeeded)
                return BadRequest("Invalid username or password");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(
                new
                {
                    user.Id,
                    user.UserName,
                    Roles = roles.ToArray(),
                }
            );
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(
                new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = roles.ToList(),
                }
            );
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return BadRequest("Username already exists.");
            }

            var user = new User { UserName = model.UserName };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            foreach (var role in model.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return BadRequest($"Role '{role}' does not exist.");
                }

                await _userManager.AddToRoleAsync(user, role);
            }

            return Ok(
                new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = model.Roles,
                }
            );
        }
    }
}
