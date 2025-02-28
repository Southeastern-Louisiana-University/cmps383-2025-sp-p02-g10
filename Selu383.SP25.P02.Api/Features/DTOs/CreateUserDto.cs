using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.P02.Api.Features.DTOs
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one role must be specified.")]
        public List<string> Roles { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
