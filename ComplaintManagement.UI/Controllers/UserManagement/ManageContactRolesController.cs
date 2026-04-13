using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.UI.Controllers.UserManagement
{
    public class ManageContactRolesController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //private readonly HttpClient _httpClient;

        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public ManageContactRolesController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var users = await client.GetFromJsonAsync<List<IdentityUser>>($"{api}/api/UserRoles/users");
            return View(users);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var model = await client.GetFromJsonAsync<ManageUserRolesDto>($"{api}/api/UserRoles/{userId}");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ManageUserRolesDto model)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var response = await client.PostAsJsonAsync($"{api}/api/UserRoles/update", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

    }
}
