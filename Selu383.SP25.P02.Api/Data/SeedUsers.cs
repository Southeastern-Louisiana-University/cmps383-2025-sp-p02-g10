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

                string adminRole = "admin";
                string adminUsername = "admin";
                string adminPassword = "Admin@123";

                // Ensure the custom role exists
                var role = await roleManager.FindByNameAsync(adminRole);
                if (role == null)
                {
                    role = new Role
                    {
                        Name = adminRole
                    };
                    var result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        Console.WriteLine("✅ Admin role seeded successfully!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Error seeding admin role:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($" - {error.Description}");
                        }
                    }
                }

                // Check if admin user already exists
                var adminUser = await userManager.FindByNameAsync(adminUsername);
                if (adminUser == null)
                {
                    var user = new User
                    {
                        UserName = adminUsername
                    };

                    var result = await userManager.CreateAsync(user, adminPassword);
                    if (result.Succeeded)
                    {
                        // Manually add the UserRole association
                        var userRole = new UserRole
                        {
                            UserId = user.Id,
                            RoleId = role.Id
                        };

                        // Add UserRole to the database
                        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                        dbContext.UserRoles.Add(userRole);
                        await dbContext.SaveChangesAsync();

                        // Add the role using UserManager
                        await userManager.AddToRoleAsync(user, adminRole);

                        Console.WriteLine("✅ Admin user seeded successfully!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Error seeding admin user:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($" - {error.Description}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ℹ️ Admin user already exists.");
                }
            }
        }
    }
    }   
