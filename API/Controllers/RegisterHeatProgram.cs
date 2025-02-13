using Api.Domain;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("heat-program")]
    [ApiController]
    public class HeatProgramControlllers : ControllerBase
    {
        private static readonly HeatProgramServices Service = new();

        [HttpPost]
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
        public IActionResult GetHeatPrograms()
        {
            return Ok(Service.All());
        }
    }
}
