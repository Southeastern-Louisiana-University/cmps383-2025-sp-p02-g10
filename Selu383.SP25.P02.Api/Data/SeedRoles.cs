using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Features.Role;
using Selu383.SP25.P02.Api.Features.User;
using Selu383.SP25.P02.Api.Features.UserRole;

namespace Selu383.SP25.P02.Api.Data
{
    public class SeedRoles
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // Look for any users.
                if (context.Roles.Any())
                {
                    return;   // DB has been seeded
                }
                context.Roles.AddRange(
                    new Role
                    {
                        Name = "Admin",

                    },
                    new Role
                    {
                        Name = "User",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
}
