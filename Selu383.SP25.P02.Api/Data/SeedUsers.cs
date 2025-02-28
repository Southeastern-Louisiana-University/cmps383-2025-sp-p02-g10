using Microsoft.AspNetCore.Identity;
using Selu383.SP25.P02.Api.Features;

namespace Selu383.SP25.P02.Api.Data
{
    public static class SeedUsers
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                string[] roles = { "Admin", "User" };

                // Ensure the custom role exists
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new Role { Name = role });
                    }
                }

                await CreateUserIfNotExists(userManager, "galkadi", "Admin");
                await CreateUserIfNotExists(userManager, "bob", "User");
                await CreateUserIfNotExists(userManager, "sue", "User");


            }
        }

        public static async Task CreateUserIfNotExists(UserManager<User> userManager, string username, string role)
        {
            if (await userManager.FindByNameAsync(username) == null)
            {
                var user = new User { UserName = username };
                await userManager.CreateAsync(user, "Password123!");
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}