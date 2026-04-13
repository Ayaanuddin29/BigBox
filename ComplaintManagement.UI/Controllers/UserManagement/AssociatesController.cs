using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.UI.Controllers.UserManagement
{
    public class AssociatesController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public AssociatesController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var response = await client.GetAsync($"{api}/api/associates");
            var data = await response.Content.ReadFromJsonAsync<List<AssociateDto>>();
            return View(data);
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var api = _config["ApiBaseUrl"];
        //    var client = _client.CreateClient();

        //    var response = await client.GetAsync($"{api}/api/associates/{id}");
        //    var data = await response.Content.ReadFromJsonAsync<AssociateDto>();
        //    return View(data);
        //}

        public async Task<IActionResult> Edit(int id)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            // Get Associate
            var response = await client.GetAsync($"{api}/api/associates/{id}");
            var data = await response.Content.ReadFromJsonAsync<AssociateDto>();

            // Get dropdown list (exclude current)
            var dropdownResponse = await client.GetAsync($"{api}/api/associates/dropdown/{id}");
            var associates = await dropdownResponse.Content.ReadFromJsonAsync<List<AssociateDto>>();

            ViewBag.AssociateList = associates;

            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AssociateDto model)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            //await client.PutAsJsonAsync($"{api}/api/associates/Update", model);

            var response = await client.PutAsJsonAsync($"{api}/api/associates", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }
    }

}
