using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace ComplaintManagement.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public AccountController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var dto = new { UserName = userName, Password = password };
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();
            var resp = await client.PostAsJsonAsync($"{apiBase}/api/auth/login", dto);

            if (!resp.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            var obj = await resp.Content.ReadFromJsonAsync<LoginResponse>();
            HttpContext.Session.SetString("JWT", obj?.Access_Token ?? string.Empty);
            HttpContext.Session.SetString("UserName", obj?.User ?? string.Empty);

            return RedirectToAction("Index", "Home"); 
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

    public class LoginResponse
    {
        public string Access_Token { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
