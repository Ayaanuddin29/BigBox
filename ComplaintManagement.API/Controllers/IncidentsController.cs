

using ComplaintManagement.Business.Interfaces.Incident;
using ComplaintManagement.Util.Models.Incident;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ComplaintManagement.API.Controllers
{
    
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentsController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpPost]
        [Route("api/[controller]/CreateIncident")]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentCreateDto incident)
        {
            var id = await _incidentService.CreateIncidentAsync(incident);
            return Ok(new { IncidentId = id });
        }

        [HttpGet]
        [Route("api/[controller]/GetIncidentList")]
        public async Task<IActionResult> GetIncidentList()
        {
            var incidents = await _incidentService.GetIncidentListAsync();
            return Ok(incidents);
        }

        [HttpGet("api/[controller]/GetIncidentById/{incId}")]
        // [Route("api/[controller]/GetIncidentById")]
        public async Task<IActionResult> GetIncidentById(string incId)
        {
            var incidents = await _incidentService.GetIncidentByIdAsync(incId);
            return Ok(incidents);
        }

        [HttpPost]
        [Route("api/[controller]/UpdateIncident")]
        public async Task<IActionResult> UpdateIncident([FromBody] IncidentUpdateDto incident)
        {
            var incidents = await _incidentService.UpdateIncidentAsync(incident);
            return Ok(incidents);
        }
        [HttpPost]
        [Route("api/[controller]/CreateIncidentActivity")]
        public async Task<IActionResult> CreateIncidentActivity([FromForm] IncidentActivityDto incidentAct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _incidentService.CreateIncidentActivityAsync(incidentAct);
            return Ok(new { actId = id });
        }

        [HttpGet("api/[controller]/GetActivitiesByIncId/{incId}")]
        // [Route("api/[controller]/GetActivitiesByIncId")]
        public async Task<IActionResult> GetActivitiesByIncId(string incId)
        {
            var incidents = await _incidentService.GetActivitiesByIncIdAsync(incId);
            return Ok(incidents);
        }

        [HttpPost]
        [Route("api/[controller]/CreateIncidentTask")]
        public async Task<IActionResult> CreateIncidentTask([FromForm] IncidentTasksDto incidentTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _incidentService.CreateIncidentTasksync(incidentTask);
            return Ok(new { newId = id });
        }

        [HttpGet("api/[controller]/GetTasksByIncId/{incId}")]
        public async Task<IActionResult> GetTasksByIncId(string incId)
        {
            var incidents = await _incidentService.GetTasksByIncIdAsync(incId);
            return Ok(incidents);
        }

        // GET EXISTING TICKET
        [HttpGet]
        [Route("api/[controller]/getticketforcopy/{id}")]
        public IActionResult GetTicketForCopy(int id)
        {
            var result = _incidentService.GetTicketForCopy(id);

            return Ok(result);
        }

        // CREATE COPY
        [HttpPost]
        [Route("api/[controller]/createcopy")]
        public IActionResult CreateCopy([FromBody] IncidentCopyViewModel model)
        {
            var newTicketId = _incidentService.CreateCopyTicket(model);

            return Ok(newTicketId);
        }

        [HttpPost]
        [Route("api/[controller]/MarkUnmarkMajorIncident/{id}")]
        public async Task<IActionResult> MarkUnmarkMajorIncident(string id)
        {
            await _incidentService.ToggleMajorIncident(id);

            return Ok(new
            {
                success = true,
                message = "Major Incident status updated successfully"
            });
        }

        [HttpPost("api/[controller]/SetInProgress/{id}")]
        public IActionResult SetInProgress(string id)
        {
            try
            {
                // Call Business Layer
                _incidentService.UpdateState(id, "2");

                return Ok(new
                {
                    success = true,
                    message = "Incident moved to In Progress"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("api/[controller]/SaveResolution")]
        public async Task<IActionResult> SaveResolution([FromForm]  ResolutionModel model, List<IFormFile> files)
        {
            // Set system fields
           // model.login_created = User.Identity.Name;

            if (model.solution_accept_or_reject == false)
            {
                model.accepted_or_rejected_on = DateTime.Now;
            }
            else if (model.solution_accept_or_reject == true)
            {
                model.accepted_or_rejected_on = DateTime.Now;
            }

            var id = await _incidentService.SaveResolution(model);

            //// File upload
            //if (files != null && files.Count > 0)
            //{
            //    foreach (var file in files)
            //    {
            //        using (var ms = new MemoryStream())
            //        {
            //            await file.CopyToAsync(ms);

            //            var attachment = new ResolutionAttachment
            //            {
            //                resol_id = id,
            //                file_name = file.FileName,
            //                uploaded_file = ms.ToArray()
            //            };

            //            await _incidentService.SaveAttachment(attachment);
            //        }
            //    }
            //}

            return RedirectToAction("ManageIncident", new { id = model.incid_no });
        }

    }
}
