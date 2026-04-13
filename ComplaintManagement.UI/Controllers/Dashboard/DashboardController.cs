using ComplaintManagement.Util.Models.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ComplaintManagement.UI.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public DashboardController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            DashboardSummaryDto model = new DashboardSummaryDto();

            try
            {
                var api = _config["ApiBaseUrl"];
                var client = _client.CreateClient();

                var response = await client.GetAsync($"{api}/api/dashboard");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<DashboardSummaryDto>(json);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(model);
        }
    }
}
