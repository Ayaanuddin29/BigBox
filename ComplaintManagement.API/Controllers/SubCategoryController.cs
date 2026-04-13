using ComplaintManagement.Business.Interfaces.Master;
using ComplaintManagement.Util.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _service;

        public SubCategoryController(ISubCategoryService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var data = await _service.GetByCategoryAsync(categoryId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubCategoryModel model)
        {
            var result = await _service.CreateAsync(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] SubCategoryModel model)
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
