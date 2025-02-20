using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Features.User;

namespace Selu383.SP25.P02.Api.Data
{
    public class SeedUsers
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // Look for any users.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }
                context.Users.AddRange(
                    new User
                    {
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
