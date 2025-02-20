using Microsoft.AspNetCore.Identity;
using Selu383.SP25.P02.Api.Features.UserRole;
namespace Selu383.SP25.P02.Api.Features.User
{
    public class User : IdentityUser<int> 
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public ICollection<UserRole.UserRole> UserRoles { get; set; }  // Navigation Property

    }
}
