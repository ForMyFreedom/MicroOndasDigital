using API.Domain;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserDTO userEntry)
        {
            var user = await _userService.Get(userEntry.Username, userEntry.Password);

            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(user);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userEntry)
        {
            try
            {
                await _userService.Register(userEntry);
                return Ok("Usuário registrado com sucesso.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
