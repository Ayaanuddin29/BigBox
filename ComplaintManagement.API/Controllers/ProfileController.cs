using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _service;

        public ProfileController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _service.GetProfileAsync(userId);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            if (model == null)
                return BadRequest();

            // DEBUG
            Console.WriteLine(model.username);

            await _service.UpdateProfileAsync(model);

            return Ok();
        }
        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments()
        {
            var data = await _service.GetDepartmentsAsync();

            return Ok(data);
        }
        [HttpGet("dropdowns")]
        public async Task<IActionResult> GetDropdowns()
        {
            return Ok(new
            {
                divisions = await _service.GetDivisionsAsync(),
                regions = await _service.GetRegionsAsync(),
                countries = await _service.GetCountriesAsync(),
                states = await _service.GetStatesAsync(),
                cities = await _service.GetCitiesAsync(),
                locations = await _service.GetLocationsAsync()
            });
        }
        [HttpGet("divisions")]
        public async Task<IActionResult> GetDivisions()
        {
            var data = await _service.GetDivisionsAsync();
            return Ok(data);
        }

        [HttpGet("regions")]
        public async Task<IActionResult> GetRegions()
        {
            var data = await _service.GetRegionsAsync();
            return Ok(data);
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var data = await _service.GetCountriesAsync();
            return Ok(data);
        }

        [HttpGet("states")]
        public async Task<IActionResult> GetStates()
        {
            var data = await _service.GetStatesAsync();
            return Ok(data);
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            var data = await _service.GetCitiesAsync();
            return Ok(data);
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations()
        {
            var data = await _service.GetLocationsAsync();
            return Ok(data);
        }

    }
}