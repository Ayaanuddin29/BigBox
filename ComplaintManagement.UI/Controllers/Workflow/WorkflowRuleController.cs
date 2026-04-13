using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComplaintManagement.UI.Controllers.Workflow
{
    public class WorkflowRuleController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public WorkflowRuleController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{apiBase}/api/WorkflowRule");
            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkflowRuleViewModel model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{apiBase}/api/WorkflowRule", content);
            return Json(new { success = response.IsSuccessStatusCode });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] WorkflowRuleViewModel model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{apiBase}/api/WorkflowRule/{model.RuleNumber}", content);
            return Json(new { success = response.IsSuccessStatusCode });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.DeleteAsync($"{apiBase}/api/WorkflowRule/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }
    }
}
