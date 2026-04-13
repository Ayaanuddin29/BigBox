using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.UI.Controllers.UserManagement
{
    public class ApproverController : Controller
    {

        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;


        public ApproverController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var result = await client.GetFromJsonAsync<List<ApproverDto>>($"{api}/api/Approver");
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApproverDto dto)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            await client.PostAsJsonAsync($"{api}/api/Approver", dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var manager = await client.GetFromJsonAsync<ApproverDto>($"{api}/api/Approver/{id}");

            if (manager == null)
                return NotFound();

            // Get available managers excluding self
            var dropdown = await client.GetFromJsonAsync<List<ApproverDto>>
                ($"{api}/api/Approver/available/{manager.Username}");

            ViewBag.AltManagers = dropdown;

            return View(manager);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApproverDto dto)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            await client.PutAsJsonAsync($"{api}/api/Approver", dto);
            return RedirectToAction("Index");
        }

    }
}
