using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComplaintManagement.UI.Controllers.Master
{
    public class ServiceController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public ServiceController(IHttpClientFactory clientFactory, IConfiguration config)
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

            var response = await client.GetAsync($"{apiBase}/api/Service/GetAll");
            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetBySubCategory(int subCategoryId)
        {
            var apiBase = _config["ApiBaseUrl"];
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{apiBase}/api/Service/GetBySubCategory/{subCategoryId}");
            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceDto model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new
            {
                SubCategoryId = model.SubCategoryId,
                Name = model.Name,
                Name_Ar = model.Name_Ar,
                login_created = model.login_created
            };

            var json = JsonConvert.SerializeObject(payload);
            var response = await client.PostAsync($"{apiBase}/api/Service",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ServiceDto model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(model);
            var response = await client.PutAsync($"{apiBase}/api/Service/Update",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.DeleteAsync($"{apiBase}/api/Service/Delete/{id}");
            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }
    }
}
