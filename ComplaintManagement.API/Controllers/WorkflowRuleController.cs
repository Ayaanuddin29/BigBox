using ComplaintManagement.Business.Interfaces.Workflow;
using ComplaintManagement.Util.Models.Workflow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowRuleController : ControllerBase
    {
        private readonly IWorkflowRuleService _service;

        public WorkflowRuleController(IWorkflowRuleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rules = await _service.GetAllAsync();
            return Ok(rules);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rule = await _service.GetByIdAsync(id);
            if (rule == null) return NotFound();
            return Ok(rule);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkflowRuleDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var login = User?.Identity?.Name ?? "System";
            var newId = await _service.CreateAsync(model, login);
            model.RuleNumber = newId;

            return CreatedAtAction(nameof(GetById), new { id = newId }, model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkflowRuleDto model)
        {
            if (id != model.RuleNumber) return BadRequest("Id mismatch");

            var login = User?.Identity?.Name ?? "System";
            var success = await _service.UpdateAsync(model, login);

            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
