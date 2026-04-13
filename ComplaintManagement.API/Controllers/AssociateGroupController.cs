using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AssociateGroupController : ControllerBase
    {
        private readonly IAssociateGroupService _service;

        public AssociateGroupController(IAssociateGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("CreateModel")]
        public async Task<IActionResult> CreateModel()
            => Ok(await _service.PrepareCreateModelAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> EditModel(int id)
            => Ok(await _service.PrepareEditModelAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(AssociateGroupDto model)
        {
            await _service.CreateAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(AssociateGroupDto model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }
    }
}
