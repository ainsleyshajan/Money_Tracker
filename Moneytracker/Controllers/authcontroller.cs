using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Contract;
using MoneyTracker.Services;

namespace MoneyTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Iauthservice _authService;

        public AuthController(Iauthservice authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            var result = await _authService.RegisterUserAsync(username, email, password);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _authService.LoginUserAsync(email, password);
            return Ok(result);
        }
    }
}