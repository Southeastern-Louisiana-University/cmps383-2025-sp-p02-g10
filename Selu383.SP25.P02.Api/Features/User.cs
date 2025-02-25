using Microsoft.AspNetCore.Identity;
namespace Selu383.SP25.P02.Api.Features
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> Roles { get; set; }  // Navigation Property

    }
}
