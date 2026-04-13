using ComplaintManagement.Business.Interfaces.Master;
using ComplaintManagement.Util.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _service;

        public MasterController(IMasterService service)
        {
            _service = service;
        }

        [HttpGet("{tableName}")]
        public async Task<IActionResult> GetAll(string tableName)
        {
            var data = await _service.GetAllAsync(tableName);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] MasterModel model)
        {
            var result = await _service.SaveAsync(model);
            return Ok(result);
        }

        [HttpDelete("{tableName}/{id}")]
        public async Task<IActionResult> Delete(string tableName, int id)
        {
            var result = await _service.DeleteAsync(id, tableName);
            return Ok(result);
        }

        [HttpGet("GetAssociates")]
        public async Task<IActionResult> GetAssociates()
        {
            var data = await _service.GetAssociates();
            return Ok(data);
        }

        [HttpGet("GetAssociateGroups")]
        public async Task<IActionResult> GetAssociateGroups()
        {
            var data = await _service.GetAssociateGroups();
            return Ok(data);
        }

    }
}
