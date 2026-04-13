using ComplaintManagement.Business.Interfaces.Incident;
using ComplaintManagement.Util.Models.Incident;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentReassignController : ControllerBase
    {
        private readonly IIncidentReassignService _service;

        public IncidentReassignController(IIncidentReassignService service)
        {
            _service = service;
        }

        [HttpGet("{incidentId}")]
        public async Task<IActionResult> GetIncident(int incidentId)
        {
            var data = await _service.GetIncidentDetails(incidentId);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost("UpdateAssignment")]
        public async Task<IActionResult> UpdateAssignment([FromBody] IncidentReassignModel model)
        {
            var result = await _service.UpdateAssignment(model);
            return Ok(result);
        }

        [HttpPost("UpdatePriority")]
        public async Task<IActionResult> UpdatePriority([FromBody] IncidentReassignModel model)
        {
            var result = await _service.UpdatePrioritySlt(model);
            return Ok(result);
        }

        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] IncidentReassignModel model)
        {
            var result = await _service.UpdateCategorization(model);
            return Ok(result);
        }
    }
}
