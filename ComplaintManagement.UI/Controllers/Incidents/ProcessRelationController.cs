using ComplaintManagement.UI.Models;
using ComplaintManagement.Util.Models.Incident;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComplaintManagement.UI.Controllers.Incidents
{
    public class ProcessRelationController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public ProcessRelationController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<IActionResult> Index(int parentId)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var tickets = await client.GetFromJsonAsync<List<TicketDto>>
                         ($"{apiBase}/api/processrelation/tickets");

           // var relationTypes = "";

            var relationTypesresponse = await client.GetAsync($"{apiBase}/api/Master/robox_m_relation_type");
            var relationTypes = await relationTypesresponse.Content.ReadFromJsonAsync<List<RelationTypeDto>>()
                        ?? new List<RelationTypeDto>();

            var relationTypesList = relationTypes
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.Id.ToString(),
                                        Text = x.Name
                                    }).ToList();

            //var relationTypes = await _client.GetFromJsonAsync<List<RelationTypeDto>>
            //                    ("api/processrelation/relationtypes");

            ViewBag.RelationTypes = relationTypesList;

            ViewBag.ParentId = parentId;

            return View(tickets);
        }
    }
}
