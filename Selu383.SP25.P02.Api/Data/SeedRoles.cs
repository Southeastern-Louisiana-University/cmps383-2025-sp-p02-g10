using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Features;

namespace Selu383.SP25.P02.Api.Data
{
    public class SeedRoles
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

                // Seed Roles
                if (!context.Roles.Any())
                {
                    await roleManager.CreateAsync(new Role { Name = "Admin" });
                    await roleManager.CreateAsync(new Role { Name = "User" });
                }
                context.SaveChanges();
            }
        }
    }
}
}
