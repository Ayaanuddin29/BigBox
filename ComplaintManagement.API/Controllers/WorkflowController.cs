using ComplaintManagement.Business.Interfaces.Workflow;
using ComplaintManagement.Util.Models.Workflow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService _service;

        public WorkflowController(IWorkflowService service)
        {
            _service = service;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] WorkflowSaveModel model)
        {
            model.LoginCreated = User.Identity?.Name ?? "system";
            int id = await _service.SaveAsync(model);
            return Ok(new { workflowId = id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpGet("list/{module}")]
        public async Task<IActionResult> List(int module)
        {
            return Ok(await _service.ListAsync(module));
        }
    }

}
