using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace API.Test.Jitsi.JWT
{
    public static class JwtHelper
    {
        /// <summary>
        /// Generates a JWT token using the provided payload and signing key.
        /// </summary>
        /// <param name="payload">The payload to include in the token.</param>
        /// <param name="signingKey">The signing key used to sign the token.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public static string GenerateJwtToken(JwtPayload payload, SecurityKey signingKey)
        {
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256);
            var securityToken = new JwtSecurityToken(new JwtHeader(credentials), payload);
            var jwtHandler = new JwtSecurityTokenHandler();

            return jwtHandler.WriteToken(securityToken);
        }

        /// <summary>
        /// Validates the given JWT token using the provided validation parameters.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <param name="validationParameters">The validation parameters.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        public static bool ValidateJwtToken(string token, TokenValidationParameters validationParameters)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                jwtHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }
    }
}
