using Microsoft.AspNetCore.Identity;
namespace Selu383.SP25.P02.Api.Features.User
{
    public class User : IdentityUser<int> 
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get;  set; }
        public ICollection<UserRole.Role> Role { get; set; }  // Navigation Property

    }
}
