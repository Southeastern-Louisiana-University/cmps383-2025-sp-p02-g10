using Selu383.SP25.P02.Api.Features.User;
using Selu383.SP25.P02.Api.Features.Role;
using Microsoft.AspNetCore.Identity;
namespace Selu383.SP25.P02.Api.Features.UserRole
{
    public class UserRole : IdentityUserRole<int>
    {
        public int UserId { get; set; }
        public User.User User { get; set; }

        public int RoleId { get; set; }
        public Role.Role Role { get; set; }
    }
}
