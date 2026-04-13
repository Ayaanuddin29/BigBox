using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;

namespace ComplaintManagement.UI.Controllers.Master
{
    public class MasterController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        //private readonly HttpClient _httpClient;
        //private readonly IConfiguration _configuration;

        public MasterController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
            //_configuration = configuration;
            //_httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"])
            //};
        }

        // Generic view
        public IActionResult Index(string tableName)
        {
            ViewBag.TableName = tableName;
            return View();
        }

        // Fetch all records
        [HttpGet]
        public async Task<IActionResult> GetAll(string tableName)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{apiBase}/api/Master/{tableName}"); 

            //var response = await _httpClient.GetAsync($"api/Master/{tableName}");
            if (!response.IsSuccessStatusCode)
                return Json(new { success = false, message = "Error fetching data" });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        // Create new record
        [HttpPost]
        public async Task<IActionResult> Create(string tableName, string name,string name_ar, string createdBy)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new { TableName = tableName, Name = name, Name_ar = name_ar, login_created = createdBy };
            var json = JsonConvert.SerializeObject(payload);
            var response = await client.PostAsync($"{apiBase}/api/Master", new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return Json(new { success = true });

            return Json(new { success = false, message = "Error creating record" });
        }

        // Update record
        [HttpPost]
        public async Task<IActionResult> Update(int id, string tableName, string name, string name_ar, bool active)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var payload = new { Id = id, TableName = tableName, Name = name, Name_ar = name_ar , Active= active };
            var json = JsonConvert.SerializeObject(payload);
            var response = await client.PostAsync($"{apiBase}/api/Master", new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return Json(new { success = true });

            return Json(new { success = false, message = "Error updating record" });
        }

        // Delete record
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, string tableName)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.DeleteAsync($"{apiBase}/api/Master/{tableName}/{id}");
            if (response.IsSuccessStatusCode)
                return Json(new { success = true });

            return Json(new { success = false, message = "Error deleting record" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAssociates()
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{apiBase}/api/Master/GetAssociates");

            if (!response.IsSuccessStatusCode)
                return Json(new { success = false, message = "Error fetching data" });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetAssociateGroups()
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{apiBase}/api/Master/GetAssociateGroups");

            if (!response.IsSuccessStatusCode)
                return Json(new { success = false, message = "Error fetching data" });

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

    }
}
