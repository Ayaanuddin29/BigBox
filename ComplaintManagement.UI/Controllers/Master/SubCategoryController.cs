using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComplaintManagement.UI.Controllers.Master
{
    public class SubCategoryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public SubCategoryController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{apiBase}/api/SubCategory/GetAll");
            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var apiBase = _config["ApiBaseUrl"];
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{apiBase}/api/SubCategory/GetByCategory/{categoryId}");

            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubCategoryDto model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new
            {
                CategoryId = model.CategoryId,
                Name = model.Name,
                Name_Ar = model.Name_Ar,
                login_created = model.CreatedBy
            };

            var json = JsonConvert.SerializeObject(payload);
            var response = await client.PostAsync($"{apiBase}/api/SubCategory",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] SubCategoryDto model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Name = model.Name,
                Name_Ar = model.Name_Ar,
                Active = model.IsActive
            };

            var json = JsonConvert.SerializeObject(payload);
            var response = await client.PutAsync($"{apiBase}/api/SubCategory/Update",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.DeleteAsync($"{apiBase}/api/SubCategory/Delete/{id}");
            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }
    }
}
