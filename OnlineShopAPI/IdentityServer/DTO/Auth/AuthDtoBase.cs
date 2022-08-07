namespace IdentityServer.DTO.Auth
{
    public abstract class AuthDtoBase
    {
        public string Email { get; set; }

        public string Name { get; set; }
    }
}