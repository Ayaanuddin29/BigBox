using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.UI.Controllers.UserManagement
{
    public class AssociateGroupController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public AssociateGroupController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var data = await client.GetFromJsonAsync<List<AssociateGroupDto>>($"{api}/api/AssociateGroup");
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var model = await client.GetFromJsonAsync<AssociateGroupDto>($"{api}/api/AssociateGroup/CreateModel");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssociateGroupDto model)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            await client.PostAsJsonAsync($"{api}/api/AssociateGroup", model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var model = await client.GetFromJsonAsync<AssociateGroupDto>($"{api}/api/AssociateGroup/{id}");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AssociateGroupDto model)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            await client.PutAsJsonAsync($"{api}/api/AssociateGroup", model);
            return RedirectToAction("Index");
        }
    }
}
