namespace SHP.AuthorizationServer.Web.DTO
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}