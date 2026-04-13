using ComplaintManagement.Business.Interfaces.Incident;
using ComplaintManagement.Business.Services.Incident;
using ComplaintManagement.Util.Models.Incident;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResolutionController : ControllerBase
    {
        private readonly IIncidentService _service;

        public ResolutionController(IIncidentService incidentService)
        {
            _service = incidentService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] ResolutionModel model)
        {
            var id = await _service.SaveResolution(model);
            return Ok(id);
        }

        [HttpGet("list/{incid_no}")]
        public async Task<IActionResult> List(int incid_no)
        {
            var data = await _service.GetResolutions(incid_no);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _service.GetResolution(id);
            return Ok(data);
        }
    }
}
