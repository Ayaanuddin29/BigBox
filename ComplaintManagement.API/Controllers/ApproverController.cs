using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproverController : ControllerBase
    {
        private readonly IApproverService _service;

        public ApproverController(IApproverService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(ApproverDto dto)
        {
            var user = User.Identity?.Name ?? "system";
            return Ok(await _service.CreateAsync(dto, user));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ApproverDto dto)
        {
            var user = User.Identity?.Name ?? "system";
            return Ok(await _service.UpdateAsync(dto, user));
        }

        [HttpGet("available/{excludeUsername}")]
        public async Task<IActionResult> GetAvailableManagers(string excludeUsername)
        {
            var data = await _service.GetAvailableManagersAsync(excludeUsername);
            return Ok(data);
        }
    }
}
