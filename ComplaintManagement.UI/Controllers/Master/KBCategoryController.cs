using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComplaintManagement.UI.Controllers.Master
{
    public class KBCategoryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public KBCategoryController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apiBase = _config["ApiBaseUrl"];
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{apiBase}/api/KBCategory/GetAll");

            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KBCategoryDto model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new
            {
                Name = model.Name,
                Description = model.Description
            };

            var json = JsonConvert.SerializeObject(payload);

            var response = await client.PostAsync($"{apiBase}/api/KBCategory",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode
                ? Json(new { success = true })
                : Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] KBCategoryDto model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };

            var json = JsonConvert.SerializeObject(payload);

            var response = await client.PutAsync($"{apiBase}/api/KBCategory/Update",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode
                ? Json(new { success = true })
                : Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.DeleteAsync($"{apiBase}/api/KBCategory/Delete/{id}");

            return response.IsSuccessStatusCode
                ? Json(new { success = true })
                : Json(new { success = false });
        }
    }
}