using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetRolesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            await _service.CreateRoleAsync(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _service.GetRoleAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleViewModel model)
        {
            await _service.UpdateRoleAsync(model);
            return Ok();
        }
    }
}
