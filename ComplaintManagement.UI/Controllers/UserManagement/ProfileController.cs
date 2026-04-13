using ComplaintManagement.UI.Models;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ComplaintManagement.UI.Controllers.UserManagement
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public ProfileController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        // ✅ GET PROFILE
        public async Task<IActionResult> Index()
        {
            string userId = HttpContext.Session.GetString("UserName") ?? "";

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var token = HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            // ✅ Load Profile
            var user = await client.GetFromJsonAsync<UserViewModel>(
                $"{api}/api/Profile/{userId}");

            // ✅ Load Departments
            var departments = await client.GetFromJsonAsync<List<DepartmentDto>>(
                $"{api}/api/Profile/departments");

            // ✅ Load All Dropdowns
            var dropdowns = await client.GetFromJsonAsync<ProfileDropdownDto>(
                $"{api}/api/Profile/dropdowns");

            // ✅ Assign ViewBag
            ViewBag.Departments = departments ?? new List<DepartmentDto>();

            ViewBag.Divisions = dropdowns?.divisions ?? new List<CommonDropdownDto>();
            ViewBag.Regions = dropdowns?.regions ?? new List<CommonDropdownDto>();
            ViewBag.Countries = dropdowns?.countries ?? new List<CommonDropdownDto>();
            ViewBag.States = dropdowns?.states ?? new List<CommonDropdownDto>();
            ViewBag.Cities = dropdowns?.cities ?? new List<CommonDropdownDto>();
            ViewBag.Locations = dropdowns?.locations ?? new List<CommonDropdownDto>();

            return View(user ?? new UserViewModel());
        }

        // ✅ UPDATE PROFILE
        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel model)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var token = HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            // ✅ Convert ViewModel to API Model
            var apiModel = new UserModel
            {
                username = model.username,
                email = model.email,
                phone = model.phone,
                address = model.address,
                zip = model.zip,

                department = model.department,
                division = model.division,
                region = model.region,
                location = model.location,
                city = model.city,
                state = model.state,
                country = model.country,

                email_alert = model.email_alert,
                sms_alert = model.sms_alert,

                secret_question = model.secret_question,
                secret_answer = model.secret_answer
            };

            var response = await client.PutAsJsonAsync(
                $"{api}/api/Profile", apiModel);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Update failed";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }
    }
}