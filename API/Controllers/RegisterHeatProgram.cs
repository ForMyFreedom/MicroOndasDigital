using API.Services;
using API.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("heat-program")]
    [ApiController]
    public class HeatProgramControlllers : ControllerBase
    {
        private static readonly HeatProgramServices Service = new();

        [HttpPost]
        [Authorize]
        public IActionResult CreateHeatProgram([FromBody] HeatProgramDto dto)
        {
            try
            {
                HeatProgram program = Service.Register(dto);
                return Ok(program);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetHeatPrograms()
        {
            return Ok(Service.All());
        }
    }
}
