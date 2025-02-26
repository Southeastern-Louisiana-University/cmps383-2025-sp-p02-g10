using Microsoft.AspNetCore.Identity;
namespace Selu383.SP25.P02.Api.Features
{
    public class Role : IdentityRole<int>
    {

        public List<UserRole> Users { get; set; }  // Navigation Property


    }
}
