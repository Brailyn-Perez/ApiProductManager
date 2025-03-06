using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_WhitJsonWebToken_JWT_.API.Customs
{
    public class Utilitys
    {
        private readonly IConfiguration _configuration;
        public Utilitys(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string encryptSHA256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                var stringBuilder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }

        }

        public string generateJWT(User user)
        {
            var userClaims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.EMail!),
             };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtConfig = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtConfig);
            Console.WriteLine($"Generated Token: {token}");

            return token;
        }


        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = null;

            try
            {
                Console.WriteLine($"Token recibido: {token}");

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidAudience = _configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!))
                }, out securityToken);

                Console.WriteLine($"SecurityToken: {securityToken}");

                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Token validation failed: {ex.Message}");
                Console.WriteLine($"Token: {token}");

                if (securityToken != null && securityToken is JwtSecurityToken jwtToken)
                {
                    Console.WriteLine($"Token Header: {string.Join(", ", jwtToken.Header)}");
                    Console.WriteLine($"Token Claims: {string.Join(", ", jwtToken.Claims.Select(c => c.Type + ": " + c.Value))}");
                }
                throw;
            }
        }


    }
}