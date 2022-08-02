using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace IdentityServer.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string ToErrorsString (this IdentityResult result)
        {
            return string.Join('\n', result.Errors.Select(x => x.Description));
        }
    } 
}
