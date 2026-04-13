using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComplaintManagement.UI.Controllers.Master
{

    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public CategoryController(IHttpClientFactory clientFactory, IConfiguration config)
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
            var response = await client.GetAsync($"{apiBase}/api/Category/GetAll");

            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new
            {
                Name = model.Name,
                Name_Ar = model.Name_Ar,
                login_created = model.CreatedBy
            };

            var json = JsonConvert.SerializeObject(payload);
            var response = await client.PostAsync($"{apiBase}/api/Category",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CategoryDto model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new
            {
                Id = model.Id,
                Name = model.Name,
                Name_Ar = model.Name_Ar,
                Active = model.IsActive
            };

            var json = JsonConvert.SerializeObject(payload);
            var response = await client.PutAsync($"{apiBase}/api/Category/Update",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(string name, string name_ar, string createdBy)
        //{
        //    var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
        //    var client = _clientFactory.CreateClient();
        //    var payload = new { Name = name, Name_Ar = name_ar, login_created = createdBy };

        //    var json = JsonConvert.SerializeObject(payload);
        //    var response = await client.PostAsync($"{apiBase}/api/Category",
        //        new StringContent(json, Encoding.UTF8, "application/json"));

        //    return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(int id, string name, string name_ar, bool active)
        //{
        //    var apiBase = _config["ApiBaseUrl"];
        //    var client = _clientFactory.CreateClient();
        //    var payload = new { Id = id, Name = name, Name_Ar = name_ar, Active = active };

        //    var json = JsonConvert.SerializeObject(payload);
        //    var response = await client.PutAsync($"{apiBase}/api/Category/Update",
        //        new StringContent(json, Encoding.UTF8, "application/json"));

        //    return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.DeleteAsync($"{apiBase}/api/Category/Delete/{id}");
            return response.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }
    }
}
