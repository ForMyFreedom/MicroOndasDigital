using API.Domain;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class UserController(UserService _userService) : ControllerBase
    {
        private readonly UserService userService = _userService;

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserDTO userEntry)
        {
            var user = await userService.Get(userEntry.Username, userEntry.Password);

            if (user == null)
            {
                SystemMessageBase error = new(new("Usuário ou senha inválidos"));
                return NotFound(error);
            }

            var token = CryptoService.GenerateToken(user);
            SystemMessage<string> message = new(token);
            return Ok(message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userEntry)
        {
            try
            {
                await userService.Register(userEntry);
                SystemMessage<string> message = new("Usuário registrado com sucesso.");
                return Ok(message);
            }
            catch (System.Exception ex)
            {
                SystemMessageBase error = new(new(ex.Message));
                return BadRequest(error);
            }
        }
    }
}
