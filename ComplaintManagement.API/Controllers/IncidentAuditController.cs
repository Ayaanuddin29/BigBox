using ComplaintManagement.Business.Interfaces.Incident;
using ComplaintManagement.Util.Models.Incident;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentAuditController : ControllerBase
    {
        private readonly IAuditService _service;

        public IncidentAuditController(IAuditService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(AuditIncidentDto model)
        {
            var id = await _service.InsertAsync(model);
            return Ok(id);
        }

        [HttpGet("{inciNo}")]
        public async Task<IActionResult> GetByIncident(int inciNo)
        {
            var data = await _service.GetByIncidentAsync(inciNo);
            return Ok(data);
        }
    }
}
