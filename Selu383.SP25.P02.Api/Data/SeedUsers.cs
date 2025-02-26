using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Features;
using Selu383.SP25.P02.Api.Features.User;

namespace Selu383.SP25.P02.Api.Data
{
    public class SeedUsers
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                // Look for any users.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }
                // Seed Users
                if (!context.Users.Any())
                {
                    // Create Admin
                    var adminUser = new User { UserName = "galkadi" };
                    await userManager.CreateAsync(adminUser, "Password123!");
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    // Create Regular Users
                    var bobUser = new User { UserName = "bob" };
                    await userManager.CreateAsync(bobUser, "Password123!");
                    await userManager.AddToRoleAsync(bobUser, "User");

                    var sueUser = new User { UserName = "sue" };
                    await userManager.CreateAsync(sueUser, "Password123!");
                    await userManager.AddToRoleAsync(sueUser, "User");
                }
                //context.Users.AddRange(
                //    new User
                //    {
                //        UserName = "galkadi",


                //    }
                //    new User
                //    {
                //        UserName = "bob",
                //        Password = "Password123!",
                //        Role = "user",
                //    }
                //    new User
                //    {
                //        UserName = "Sue",
                //        Password = "Password123!",
                //        Role = "user",
                //    }
                //);
                context.SaveChanges();
            }
        }
    }
}
