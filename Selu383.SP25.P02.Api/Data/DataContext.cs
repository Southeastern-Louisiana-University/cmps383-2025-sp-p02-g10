using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.P02.Api.Features.Theaters;
using Selu383.SP25.P02.Api.Features;

namespace Selu383.SP25.P02.Api.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Theater> Theaters { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userRoleBuilder = builder.Entity<UserRole>();

            userRoleBuilder.HasKey(x => new { x.UserId, x.RoleId });

            userRoleBuilder.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);

            userRoleBuilder.HasOne(x => x.User).WithMany(x => x.Roles).HasForeignKey(x => x.UserId);
        }
    }
}
