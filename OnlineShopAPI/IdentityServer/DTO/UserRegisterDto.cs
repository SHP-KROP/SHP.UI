using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTO
{
    public class UserRegisterDto
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
