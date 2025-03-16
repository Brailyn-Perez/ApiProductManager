using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManager.BL.DTOS.User;
using ProductManager.BL.DTOS.User.CreateUserDTO;
using ProductManager.BL.Interfaces.User;
using System.Text.Json;

namespace ProductManager.API.Controllers.User
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccessController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUsersDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginUserAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("log")]
        public async Task<IActionResult> GetLog()
        {
            string logFilePath = "user_registration_log.json";
            if (!System.IO.File.Exists(logFilePath))
            {
                return NotFound("Log file not found.");
            }

            var logContent = await System.IO.File.ReadAllTextAsync(logFilePath);
            if (string.IsNullOrEmpty(logContent))
            {
                return NoContent();
            }

            var logEntries = JsonSerializer.Deserialize<object>(logContent);
            return Ok(logEntries);
        }
    }
}
