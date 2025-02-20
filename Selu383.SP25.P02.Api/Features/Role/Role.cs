using Microsoft.AspNetCore.Identity;
using Selu383.SP25.P02.Api.Features.UserRole;
namespace Selu383.SP25.P02.Api.Features.Role
{
    public class Role : IdentityRole<int>
    {
        public int Id { get; set; }

        public User.User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserRole.UserRole> UserRoles { get; set; }  // Navigation Property


    }
}
