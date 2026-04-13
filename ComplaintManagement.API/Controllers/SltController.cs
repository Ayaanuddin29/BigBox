using ComplaintManagement.Business.Interfaces.SLM;
using ComplaintManagement.Util.Models.SLM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SltController : ControllerBase
    {
        private readonly ISltService _service;

        public SltController(ISltService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SltDto dto)
        {
            var login = "Admin";
            int id = await _service.CreateAsync(dto, login);
            return Ok(new { success = true, id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SltDto dto)
        {
            dto.SlaId = id;
            var login = "Admin";
            return Ok(new { success = await _service.UpdateAsync(dto, login) });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(new { success = await _service.DeleteAsync(id) });
        }
    }
}
