using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Data;
using Selu383.SP25.P02.Api.Features;
using Selu383.SP25.P02.Api.Features.Theaters;

namespace Selu383.SP25.P02.Api.Controllers
{
    [Route("api/theaters")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        private readonly DbSet<Theater> theaters;
        private readonly DataContext dataContext;
        private readonly UserManager<User> _userManager;

        public TheatersController(DataContext dataContext, UserManager<User> userManager)
        {
            this.dataContext = dataContext;
            theaters = dataContext.Set<Theater>();
            _userManager = userManager;
        }

        [HttpGet]
        public IQueryable<TheaterDto> GetAllTheaters()
        {
            return GetTheaterDtos(theaters);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TheaterDto> GetTheaterById(int id)
        {
            var result = GetTheaterDtos(theaters.Where(x => x.Id == id)).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Only Admins can create theaters
        public ActionResult<TheaterDto> CreateTheater(TheaterDto dto)
        {
            if (IsInvalid(dto))
            {
                return BadRequest("Invalid theater data.");
            }

            var theater = new Theater
            {
                Name = dto.Name,
                Address = dto.Address,
                SeatCount = dto.SeatCount,
                managerId = dto.managerId,
            };
            theaters.Add(theater);

            dataContext.SaveChanges();

            dto.Id = theater.Id;

            return CreatedAtAction(nameof(GetTheaterById), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")] // Only Admins can create theaters
        public async Task<ActionResult<TheaterDto>> UpdateTheaterAsync(int id, TheaterDto dto)
        {
            if (IsInvalid(dto))
            {
                return BadRequest();
            }

            var theater = theaters.FirstOrDefault(x => x.Id == id);
            if (theater == null)
            {
                return NotFound();
            }

            if (dto.managerId != null)
            {
                var currentUser = await _userManager.GetUserAsync(User); // Assumes a method to retrieve the logged-in user's ID

                if (User.IsInRole("Admin"))
                {
                    // Admin can always change the ManagerId
                    theater.managerId = dto.managerId;
                }
                else
                {
                    // Regular user can only change ManagerId if they are the current manager
                    if (theater.Manager.Id != currentUser.Id)
                    {
                        return Forbid(); // The user is not allowed to change the ManagerId
                    }
                    else
                    {
                        // If they are the current manager, allow them to modify ManagerId (even to null)
                        theater.managerId = dto.managerId;
                    }
                }
            }

            theater.Name = dto.Name;
            theater.Address = dto.Address;
            theater.SeatCount = dto.SeatCount;
            theater.managerId = dto.managerId;
            dataContext.SaveChanges();

            dto.Id = theater.Id;

            return Ok(dto);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")] // Only Admins can create theaters
        public async Task<ActionResult> DeleteTheaterAsync(int id)
        {
            var theater = theaters.FirstOrDefault(x => x.Id == id);
            if (theater == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User); // Get the current logged-in user’s ID

            // Check if the user is either an Admin or the manager of the theater
            if (User.IsInRole("Admin") || theater.Manager.Id == currentUser.Id)
            {
                // Proceed with deletion if the user is an Admin or the manager
                theaters.Remove(theater);

                // Save changes to the database
                dataContext.SaveChanges();

                return Ok(); // Successfully deleted
            }

            // If the user is neither an Admin nor the Manager, return 403 Forbidden
            return Forbid(); // Forbidden: User does not have permission to delete the theater
        }

        private static bool IsInvalid(TheaterDto dto)
        {
            return string.IsNullOrWhiteSpace(dto.Name)
                || dto.Name.Length > 120
                || string.IsNullOrWhiteSpace(dto.Address)
                || dto.SeatCount <= 0;
        }

        private static IQueryable<TheaterDto> GetTheaterDtos(IQueryable<Theater> theaters)
        {
            return theaters.Select(x => new TheaterDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                SeatCount = x.SeatCount,
                managerId = x.Manager.Id,
            });
        }
    }
}
