using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BeSmart.Server.Persistence
{
    public class Options
    {
        public const string Issuer = "AuthServer";
        public const string Audience = "TokenClient";
        const string SecretKey = "9hbwk2943pek2mq8b26l1721bm568ynl";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    }
}
