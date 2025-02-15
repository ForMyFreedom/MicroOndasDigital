using API.Services;
using API.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Data;

namespace API.Controllers
{
    [Route("heat-program")]
    [ApiController]
    public class HeatProgramControlllers(HeatProgramServices _heatService) : ControllerBase
    {
        private readonly HeatProgramServices heatService = _heatService;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateHeatProgram([FromBody] HeatProgramDto dto)
        {
            try
            {
                HeatProgram program = await heatService.Register(dto);
                SystemMessage<HeatProgram> message = new(program);
                return Ok(message);
            }
            catch (Exception ex)
            {
                SystemMessageBase error = new(new(ex.Message));
                return BadRequest(error);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetHeatPrograms()
        {
            SystemMessage<List<HeatProgram>> message = new(heatService.All());
            return Ok(message);
        }
    }
}
