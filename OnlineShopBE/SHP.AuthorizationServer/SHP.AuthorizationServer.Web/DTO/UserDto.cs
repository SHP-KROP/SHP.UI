using System.ComponentModel.DataAnnotations;

namespace SHP.AuthorizationServer.Web.DTO
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

        [Required]
        public string RefreshToken { get; set; }
    }
}
