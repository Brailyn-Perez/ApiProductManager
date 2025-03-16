
using ProductManager.BL.DTOS.User;
using System.Security.Claims;

namespace ProductManager.BL.Interfaces.User
{
    public interface IJwtService
    {
        public string encryptSHA256(string text);
        public string generateJWT(LoginUsersDTO user);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
