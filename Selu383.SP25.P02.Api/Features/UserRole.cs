using Microsoft.AspNetCore.Identity;
using Selu383.SP25.P02.Api.Features.User;
namespace Selu383.SP25.P02.Api.Features;

public class UserRole : IdentityUserRole<int>
{
    public User User { get; set; }
    public Role Role { get; set; }
}
