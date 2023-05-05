namespace SHP.AuthorizationServer.Web.Contracts
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string[] Errors { get; set; }
    }
}