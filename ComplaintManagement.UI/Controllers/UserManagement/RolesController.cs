using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http;


namespace ComplaintManagement.UI.Controllers.UserManagement
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public RolesController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var response = await client.GetAsync($"{api}/api/roles");

            if (!response.IsSuccessStatusCode)
                return View(new List<RoleViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var roles = JsonSerializer.Deserialize<List<RoleViewModel>>(
                json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            if (!ModelState.IsValid) return View(model);

            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync($"{api}/api/roles", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unable to create role");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var response = await client.GetAsync($"{api}/api/roles/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var role = JsonSerializer.Deserialize<RoleViewModel>(
                json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            if (!ModelState.IsValid) return View(model);

            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"{api}/api/roles", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Update failed");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
