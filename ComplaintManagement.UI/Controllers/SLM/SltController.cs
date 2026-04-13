using ComplaintManagement.Util.Models.SLM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComplaintManagement.UI.Controllers.SLM
{
    public class SltController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public SltController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var res = await client.GetAsync($"{api}/api/Slt");
            var json = await res.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var res = await client.GetAsync($"{api}/api/Slt/{id}");

            if (!res.IsSuccessStatusCode)
                return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<SltDto>(json);

            return Json(dto); 
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SltDto dto)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage res;

            if (dto.SlaId == 0)
                res = await client.PostAsync($"{api}/api/Slt", content);
            else
                res = await client.PutAsync($"{api}/api/Slt/{dto.SlaId}", content);

            return Json(new { success = res.IsSuccessStatusCode });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var api = _config["ApiBaseUrl"];
            var client = _client.CreateClient();

            var res = await client.DeleteAsync($"{api}/api/Slt/{id}");
            return Json(new { success = res.IsSuccessStatusCode });
        }
    }
}
