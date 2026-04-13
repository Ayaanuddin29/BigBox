using ComplaintManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComplaintManagement.UI.Controllers.Master
{
    public class KBStatusController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public KBStatusController(IHttpClientFactory clientFactory, IConfiguration config)
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

            var response = await client.GetAsync($"{apiBase}/api/KBStatus/GetAll");

            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var result = await response.Content.ReadAsStringAsync();

            return Content(result, "application/json");
        }
    }
}