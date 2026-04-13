using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.UI.Controllers.UserManagement
{
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public UsersController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();
            var users = await client.GetFromJsonAsync<List<UserListDto>>($"{api}/api/users");
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();
            //await client.PostAsJsonAsync($"{api}/api/users", dto);
            var response = await client.PostAsJsonAsync($"{api}/api/users", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
