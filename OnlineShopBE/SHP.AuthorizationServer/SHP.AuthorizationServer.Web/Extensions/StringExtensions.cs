using System.Text;

namespace SHP.AuthorizationServer.Web.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string @this)
        {
            return Encoding.UTF8.GetBytes(@this);
        }
    }
}
