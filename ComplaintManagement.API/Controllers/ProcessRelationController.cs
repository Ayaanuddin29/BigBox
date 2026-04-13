using ComplaintManagement.Business.Interfaces.Incident;
using ComplaintManagement.Util.Models.Incident;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessRelationController : ControllerBase
    {
        private readonly IProcessRelationService _service;

        public ProcessRelationController(IProcessRelationService service)
        {
            _service = service;
        }        

        [HttpGet("tickets")]
        public async Task<IActionResult> GetTickets()
        {
            var data = await _service.GetTickets();
            return Ok(data);
        }

        [HttpPost("link")]
        public async Task<IActionResult> LinkTickets([FromBody] RelationRequest request)
        {
            await _service.AddRelations(request.ParentId,
                                        request.ChildIds,
                                        request.RelationTypeId);

            return Ok();
        }

        [HttpGet("relations/{parentId}")]
        public async Task<IActionResult> GetRelations(int parentId)
        {
            var data = await _service.GetRelations(parentId);

            return Ok(data);
        }

    }
}
