using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTO
{
    public class UserDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
