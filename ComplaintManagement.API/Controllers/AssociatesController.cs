using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociatesController : ControllerBase
    {
        private readonly IAssociateService _service;

        public AssociatesController(IAssociateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(AssociateDto model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }

        [HttpGet("dropdown/{excludeId}")]
        public async Task<IActionResult> GetForDropdown(int excludeId)
        {
            var data = await _service.GetAllAsync();
            var filtered = data.Where(x => x.AssociateId != excludeId);
            return Ok(filtered);
        }


    }

}
