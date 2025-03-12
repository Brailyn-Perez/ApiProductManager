using API_WhitJsonWebToken_JWT_.API.Customs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManager.API.DTOS.User.CreateUserDTO;
using ProductManager.API.DTOS.User;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ProductManager.API.Controllers.User
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Utilitys _utilitys;
        public AccessController(ApplicationDbContext context, Utilitys utilitys)
        {
            _context = context;
            _utilitys = utilitys;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(CreateUserDTO createUser)
        {

            var user = new Entities.User.User()
            {
                Name = createUser.Name,
                EMail = createUser.EMail,
                Password = _utilitys.encryptSHA256(createUser.Password)
            };

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();

            if (user.Id != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUsersDTO loginUsers)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }
            var findUser = await _context.users
                .Where(
                x => x.EMail == loginUsers.EMail &&
                x.Password == _utilitys.encryptSHA256(loginUsers.Password)
                ).FirstOrDefaultAsync();

            if (findUser == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilitys.generateJWT(findUser) });
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] string token)
        {
            var principal = _utilitys.GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return BadRequest("Invalid token");
            }

            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("Invalid token");
            }

            var user = new Entities.User.User()
            {
                Id = int.Parse(userId),
                EMail = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
            };

            var newToken = _utilitys.generateJWT(user);
            return Ok(new { token = newToken });

        }
    }
}
