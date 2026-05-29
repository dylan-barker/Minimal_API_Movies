using Microsoft.IdentityModel.Tokens;

namespace Minimal_API_Movies.Utils
{
    public class KeysHandler
    {
        public const string OurIssuer = "our-app";
        private const string KeysSection = "Authentication:Schemes:Bearer:SigningKeys";
        private const string KeySection_Issuer = "Issuer";
        private const string KeySection_Value = "Value";

        public static IEnumerable<SecurityKey> GetKey(IConfiguration configuration)
            => GetKey(configuration, OurIssuer);

        public static IEnumerable<SecurityKey> GetKey(IConfiguration configuration, string issuer)
        {
            var signingKey = configuration.GetSection(KeysSection)
                .GetChildren()
                .SingleOrDefault(key => key[KeySection_Issuer] == issuer);

            if (signingKey is not null && signingKey[KeySection_Value] is string secretKey)
            {
                yield return new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
            }
        }

        public static IEnumerable<SecurityKey> GetAllKeys(IConfiguration configuration)
        {
            var signingKeys = configuration.GetSection(KeysSection)
                .GetChildren();

            foreach (var signingKey in signingKeys)
            {
                if (signingKey[KeySection_Value] is string secretKey)
                {
                    yield return new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
                }
            }
        }
    }
}
