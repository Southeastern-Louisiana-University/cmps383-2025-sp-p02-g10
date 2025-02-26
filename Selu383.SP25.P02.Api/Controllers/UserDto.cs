namespace Selu383.SP25.P02.Api.Features.Users
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty; 
        public string UserName { get; set; } = string.Empty;
        public string? Username { get; internal set; }
        public string[] Roles { get; set; } = []; 
    }
}