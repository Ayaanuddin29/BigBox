using ComplaintManagement.Business.Interfaces.Master;
using ComplaintManagement.Util.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("GetBySubCategory/{subCategoryId}")]
        public async Task<IActionResult> GetBySubCategory(int subCategoryId)
        {
            var result = await _service.GetBySubCategoryAsync(subCategoryId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceModel model)
        {
            var result = await _service.CreateAsync(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ServiceModel model)
        {
            var result = await _service.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
