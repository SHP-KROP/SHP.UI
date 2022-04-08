using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTO
{
    public class UserLogInDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
