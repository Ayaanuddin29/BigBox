using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using ComplaintManagement.Util.Models.Workflow;

namespace ComplaintManagement.UI.Controllers.Workflow
{
    public class WorkflowController : Controller
    {
        //private readonly HttpClient _httpClient;

        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public WorkflowController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        // 🔹 Designer Page
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> Save([FromBody] WorkflowSaveModel model)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var response = await client.PostAsJsonAsync($"{api}/api/workflow/save", model);
            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        // 🔹 Edit Workflow
        public async Task<IActionResult> Edit(int id)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var response = await client.GetAsync($"{api}/api/workflow/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<WorkflowSaveModel>(
                json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View("Index", model);
        }

        // 🔹 Workflow List
        public async Task<IActionResult> List(int module = 1)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var response = await client.GetAsync($"{api}/api/workflow/list/{module}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonSerializer.Deserialize<List<WorkflowMasterModel>>(
                json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(list);
        }
    }
}
