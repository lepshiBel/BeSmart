using System.Text;

namespace BeSmart.Application
{
    public class SomeOptions
    {
        public const string Issuer = "AuthServer";
        public const string Audience = "TokenClient";
        const string SecretKey = "9hbwk2943pek2mq8b26l1721bm568ynl";
        public static byte[] GenerateBytes() =>
            Encoding.ASCII.GetBytes(SecretKey);
    }
}
