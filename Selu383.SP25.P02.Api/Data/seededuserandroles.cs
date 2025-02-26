using Microsoft.AspNetCore.Identity;
using Selu383.SP25.P02.Api.Features.Roles;
using Selu383.SP25.P02.Api.Features.Users;

namespace Selu383.SP25.P02.Api.Data
{
    public static class SeedUsersAndRoles
    {
        private static readonly string DefaultPassword = "Password123!"; 
        public static async Task EnsureSeededAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            var roles = new[] { "Admin", "User" };
            var users = new (string Username, string Role)[]
            {
                ("galkadi", "Admin"),
                ("bob", "User")
             
            };

       
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new Role { Name = role });
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to seed role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            // Ensure Users Exist
            foreach (var (username, role) in users)
            {
                await CreateUserIfNotExists(userManager, username, role);
            }
        }

        private static async Task CreateUserIfNotExists(UserManager<User> userManager, string username, string role)
        {
            if (await userManager.FindByNameAsync(username) == null)
            {
                var user = new User { UserName = username };
                var createResult = await userManager.CreateAsync(user, DefaultPassword);
                if (!createResult.Succeeded)
                {
                    throw new Exception($"Failed to create user '{username}': {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }

                var roleResult = await userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign role '{role}' to user '{username}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}