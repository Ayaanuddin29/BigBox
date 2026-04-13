using ComplaintManagement.UI.Models;
using ComplaintManagement.Util.Models.Incident;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Web;

namespace ComplaintManagement.UI.Controllers.Incidents
{
    public class IncidentsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        [ActivatorUtilitiesConstructor]
        public IncidentsController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            //if (!ModelState.IsValid)
            //    return View(IncidentListItemViewModel);

            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();            
            var response = await client.GetAsync($"{apiBase}/api/incidents/GetIncidentList");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to fetch incident list";
                return View(new List<IncidentListItemViewModel>());
            }

            var incidents = await response.Content.ReadFromJsonAsync<List<IncidentListItemViewModel>>();
            return View(incidents);
        }

        public IActionResult Create()
        {
            return View(new IncidentCreateViewModel());
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentCreateViewModel incidentCreateViewModel)
        {
            if (!ModelState.IsValid)
                return View(incidentCreateViewModel);

            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();
            
            // Map MVC form DTO to API DTO
            var dto = new IncidentCreateDto
            {
                //TypeId = incidentCreateViewModel.Category,
                //SourceId = incidentCreateViewModel.Category,
                AffectedUserId = HttpContext.Session.GetString("UserName") ?? "Guest",
                CreatedByUserId = HttpContext.Session.GetString("UserName") ?? "Guest",
                //Phone = incidentCreateViewModel.Caller ?? "string",
                //Mobile = incidentCreateViewModel.Caller ?? "string",
                ClassificationId = incidentCreateViewModel.Category,
                CategoryId = incidentCreateViewModel.Category,
                SubcategoryId = incidentCreateViewModel.SubCategory,
                ServiceId = incidentCreateViewModel.Service,
                StatusId = incidentCreateViewModel.Category,
                PriorityId = incidentCreateViewModel.Priority,
                UrgencyId = incidentCreateViewModel.Urgency,
                ImpactId = incidentCreateViewModel.Impact,
                Summary = incidentCreateViewModel.ShortDescription ?? "static summary",
                Description = incidentCreateViewModel.Description ?? "static description",
                AssignedByAssociateId = incidentCreateViewModel.AssignedTo,
                AssignedToAssociateId = incidentCreateViewModel.AssignedTo,
                AssignedToGroupId = incidentCreateViewModel.AssignmentGroup,
                //Comp = incidentCreateViewModel.Category,
                LoginCreated = incidentCreateViewModel.Caller ?? "admin",
                Longitude = incidentCreateViewModel.Caller ?? "0.0",
                Latitude = incidentCreateViewModel.Caller ?? "0.0",
                Column1 = incidentCreateViewModel.Caller ?? "extra1",
                Column2 = incidentCreateViewModel.Caller ?? "extra2",
                Column3 = incidentCreateViewModel.Caller ?? "extra3"
            };

            // Call your Web API
            var response = await client.PostAsJsonAsync($"{apiBase}/api/incidents/CreateIncident", dto);

            try
            {
                // 🔥 2️⃣ Get Created Incident ID (IMPORTANT)
                var createdResponse = await response.Content
                    .ReadFromJsonAsync<CreateIncidentResponseDto>();

                var createdIncidentId = createdResponse.IncidentId;
                // 🔥 3️⃣ Call Insert Audit API
                var auditDto = new AuditIncidentDto
                {
                    Incid_No = createdIncidentId,
                    ActionType = "IncidentCreated",
                    Module = "Incident",
                    Title = $"Incident {createdIncidentId} Created",
                    Description = $"Incident created with Priority: <b>{incidentCreateViewModel.PriorityText}</b> | Category: <b>{incidentCreateViewModel.CategoryText}</b>",
                    Login_Created = HttpContext.Session.GetString("UserName") ?? "Guest",
                    Rec_Created = DateTime.Now
                };

                await client.PostAsJsonAsync($"{apiBase}/api/IncidentAudit", auditDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling Audit API: " + ex.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Incident created successfully";
                return RedirectToAction("Index"); // or list page
            }

            ModelState.AddModelError("", "Error creating incident");
            return View(incidentCreateViewModel);
        }

        public async Task<IActionResult> ManageAsync(int id)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');

            #region GetIncidentDetails
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{apiBase}/api/incidents/GetIncidentById/{id}");
            var incident = await response.Content.ReadFromJsonAsync<IncidentDto>();

            var responseActivity = await client.GetAsync($"{apiBase}/api/incidents/GetActivitiesByIncId/{id}");
            //var activity = await responseActivity.Content.ReadFromJsonAsync<List<ActivityReadDto>>();
            var activityRead = await responseActivity.Content.ReadFromJsonAsync<List<Models.ActivityReadDto>>();
            //var activityRead = activities?.FirstOrDefault();

            var responseTask = await client.GetAsync($"{apiBase}/api/incidents/GetTasksByIncId/{id}");
            var TaskRead = await responseTask.Content.ReadFromJsonAsync<List<Models.IncidentTaskReadDto>>();

            #endregion

            #region Category
            /*Get Category Api call */
            var catresponse = await client.GetAsync($"{apiBase}/api/Category/GetAll");
            var catList = await catresponse.Content.ReadFromJsonAsync<List<CategoryDto>>() ?? new List<CategoryDto>();
            /**** End *******/
            #endregion

            #region SubCategory
            /*Get Category Api call */
            var subcatresponse = await client.GetAsync($"{apiBase}/api/SubCategory/GetAll");
            var subcatList = await subcatresponse.Content.ReadFromJsonAsync<List<SubCategoryDto>>() ?? new List<SubCategoryDto>();
            /**** End *******/
            #endregion

            #region Service
            /*Get Service Api call */
            var serviceresponse = await client.GetAsync($"{apiBase}/api/Service/GetAll");
            var serviceList = await serviceresponse.Content.ReadFromJsonAsync<List<ServiceDto>>() ?? new List<ServiceDto>();
            /**** End *******/
            #endregion
            var incidentauditlogs = new List<AuditIncidentDto>();
            try
            {
                var auditresponse = await client.GetAsync($"{apiBase}/api/IncidentAudit/{id}");

                if (response.IsSuccessStatusCode)
                {
                    incidentauditlogs = await auditresponse.Content.ReadFromJsonAsync<List<AuditIncidentDto>>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling Audit API: " + ex.Message);
            }

            // Build impact static dropdown list
            var impactOptions = new List<SelectListItem>
     {
         new SelectListItem { Value = "1", Text = "High" },
         new SelectListItem { Value = "2", Text = "Medium" }

     };

            // Mark selected impact
            foreach (var option in impactOptions)
            {
                option.Selected = (option.Value == incident.Impact); // assuming IncidentDto has impact property
            }
            // Build Prioritys static dropdown list
            var prioritysOptions = new List<SelectListItem>
     {
         new SelectListItem { Value = "1", Text = "Critical" },
         new SelectListItem { Value = "2", Text = "High" },
         new SelectListItem { Value = "3", Text = "Medium" },
         new SelectListItem { Value = "4", Text = "Low" }

     };
            // Mark selected Prioritys
            foreach (var option in prioritysOptions)
            {
                option.Selected = (option.Value == incident.Priority); // assuming IncidentDto has Priority property
            }
            // Build impact static dropdown list
            var UrgencyOptions = new List<SelectListItem>
     {
         new SelectListItem { Value = "1", Text = "High" },
         new SelectListItem { Value = "2", Text = "Critical" },
         new SelectListItem { Value = "1002", Text = "Low" }

     };
            // Mark selected impact
            foreach (var option in UrgencyOptions)
            {
                option.Selected = (option.Value == incident.Urgency); // assuming IncidentDto has impact property
            }

            // Build States static dropdown list
            var StatesOptions = new List<SelectListItem>
     {
         new SelectListItem { Value = "1", Text = "New" },
         new SelectListItem { Value = "2", Text = "InProgress" },
         new SelectListItem { Value = "3", Text = "Resolved" },
         new SelectListItem { Value = "4", Text = "Assigned" }

     };
            // Mark selected States
            foreach (var option in StatesOptions)
            {
                option.Selected = (option.Value == incident.State); // assuming IncidentDto has States property
            }

            //impactOptions.FirstOrDefault(o => o.Value == incident.Impact)?.Selected = true;
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to fetch incident list";
                return View(new List<IncidentDto>());
            }
            // Build ViewModel
            var viewModel = new ManageIncidentViewModel
            {
                Number = incident.Number,
                Caller = incident.Caller,
                Category = incident.Category,
                SubCategory = incident.SubCategory,
                Service = incident.Service,
                ShortDescription = incident.ShortDescription,
                Description = incident.Description,
                AssignedTo = incident.AssignedTo,
                // Map Categories to SelectListItem
                Categorys = catList.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = (c.Id == incident.Category)
                }).ToList(),
                // Map SubCategories to SelectListItem
                SubCategorys = subcatList.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = (c.Id == incident.SubCategory)
                }).ToList(),
                // Map Services to SelectListItem
                Services = serviceList.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = (c.Id == incident.Service)
                }).ToList(),
                Impacts = impactOptions,
                Urgencys = UrgencyOptions,
                Prioritys = prioritysOptions,
                States = StatesOptions

            };

            var pageVm = new ManageIncPageViewModel
            {
                formManageInc = viewModel,
                formActivityRead = activityRead,
                formIncActivity = new ActivityDto(), // or null if not needed
                formTaskReadDto = TaskRead,
                formIncidentAuditDto= incidentauditlogs
            };



            return View(pageVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateIncident(ManageIncPageViewModel manageIncPageViewModel)
        {
            var manageincidentViewModel = manageIncPageViewModel.formManageInc;
            if (!ModelState.IsValid)
                return View(manageincidentViewModel);

            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();
            // Call your Web API
            var dto = new IncidentUpdateDto
            {
                incidentId = manageincidentViewModel.Number,
                CategoryId = manageincidentViewModel.Category,
                SubcategoryId = manageincidentViewModel.SubCategory,
                StatusId = int.Parse(manageincidentViewModel.State),
                PriorityId = int.Parse(manageincidentViewModel.Priority),
                UrgencyId = int.Parse(manageincidentViewModel.Urgency),
                ImpactId = int.Parse(manageincidentViewModel.Impact),
                Summary = manageincidentViewModel.ShortDescription,
                Description = manageincidentViewModel.Description,
                AssignedToAssociateId = manageincidentViewModel.AssignedTo


            };
            var response = await client.PostAsJsonAsync($"{apiBase}/api/incidents/UpdateIncident", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Incident " + manageincidentViewModel.Number + " updated successfully";
                return RedirectToAction("Manage", "Incidents", new { id = manageincidentViewModel.Number }); // back to page

            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIncidentActivity(ManageIncPageViewModel manageIncPageViewModel)
        {
            var activityViewModel = manageIncPageViewModel.formIncActivity;
            if (!ModelState.IsValid)
                return View(manageIncPageViewModel);
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            using var form = new MultipartFormDataContent();

            form.Add(new StringContent(activityViewModel.Incid.ToString()), "Incid");
            form.Add(new StringContent(activityViewModel.Summary ?? ""), "Summary");
            form.Add(new StringContent(activityViewModel.Details ?? ""), "Details");
            //form.Add(new StringContent(activityViewModel.Rec_Created.ToString("o")), "Rec_Created");

            // Add files
            if (activityViewModel.Files != null && activityViewModel.Files.Any())
            {


                foreach (var file in activityViewModel.Files)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var fileContent = new ByteArrayContent(ms.ToArray());
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    // "Files" must match the property name in your IncidentActivityDto
                    form.Add(fileContent, "Files", file.FileName);

                }
            }
            else
            {
                // Add an empty field so API sees "Files"
                form.Add(new StringContent(string.Empty), "Files");
            }

            var response = await client.PostAsync($"{apiBase}/api/incidents/CreateIncidentActivity", form);
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Incident " + activityViewModel.Incid + " Activity Created";
                return RedirectToAction("Manage", "Incidents", new { id = activityViewModel.Incid }); // back to page

            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIncidentTask(ManageIncPageViewModel manageIncPageViewModel)
        {
            var taskViewModel = manageIncPageViewModel.formIncidentTasksDto;
            if (!ModelState.IsValid)
                return View(manageIncPageViewModel);
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            // Map MVC form DTO to API DTO  
            using var form = new MultipartFormDataContent();
            //form.Add(new StringContent(taskViewModel.Incid.ToString()), "Incid");

            if (taskViewModel.AssignedToAssociate.HasValue)
                form.Add(new StringContent(taskViewModel.AssignedToAssociate.Value.ToString()), "AssignedToAssociate");

            if (taskViewModel.AssignedToGroup.HasValue)
                form.Add(new StringContent(taskViewModel.AssignedToGroup.Value.ToString()), "AssignedToGroup");



            form.Add(new StringContent(taskViewModel.Incid?.ToString() ?? "0"), "Incid");
            form.Add(new StringContent(taskViewModel.AssignedToAssociate?.ToString() ?? string.Empty), "AssignedToAssociate");
            form.Add(new StringContent(taskViewModel.AssignedToGroup?.ToString() ?? string.Empty), "AssignedToGroup");
            form.Add(new StringContent(taskViewModel.Comp.ToString()), "Comp");
            form.Add(new StringContent(taskViewModel.TaskSummary ?? string.Empty), "TaskSummary");
            form.Add(new StringContent(taskViewModel.TaskDetails1 ?? string.Empty), "TaskDetails1");
            form.Add(new StringContent(taskViewModel.TaskCreatedOn.ToString() ?? string.Empty), "TaskCreatedOn");
            form.Add(new StringContent(taskViewModel.TotTimeSpendDays?.ToString() ?? "0"), "TotTimeSpendDays");
            form.Add(new StringContent(taskViewModel.TotTimeSpentHours?.ToString() ?? "0"), "TotTimeSpentHours");
            form.Add(new StringContent(taskViewModel.TotTimeSpentMin?.ToString() ?? "0"), "TotTimeSpentMin");
            form.Add(new StringContent(taskViewModel.TaskStatus?.ToString() ?? "0"), "TaskStatus");
            form.Add(new StringContent(taskViewModel.TaskReqStartDate?.ToString() ?? string.Empty), "TaskReqStartDate");
            form.Add(new StringContent(taskViewModel.TaskReqEndDate?.ToString() ?? string.Empty), "TaskReqEndDate");
            form.Add(new StringContent(taskViewModel.TaskType?.ToString() ?? "0"), "TaskType");

            //Add files
            if (taskViewModel.Files != null && taskViewModel.Files.Any())
            {


                foreach (var file in taskViewModel.Files)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    var fileContent = new ByteArrayContent(ms.ToArray());
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    // "Files" must match the property name in your IncidentActivityDto
                    form.Add(fileContent, "Files", file.FileName);

                }
            }

            var response = await client.PostAsync($"{apiBase}/api/incidents/CreateIncidentTask", form);
            if (response.IsSuccessStatusCode)
            {

                //var responseTask = await client.GetAsync($"{apiBase}/api/incidents/GetTasksByIncId/{taskViewModel.Incid}");
                //var TaskRead = await responseTask.Content.ReadFromJsonAsync<List<Models.IncidentTaskReadDto>>();
                //return PartialView("_TaskGrid", TaskRead);

                TempData["Message"] = "Incident " + taskViewModel.Incid + " Task Created";
                return RedirectToAction("Manage", "Incidents", new { id = taskViewModel.Incid }); // back to page

            }
            return View();

        }

        // OPEN CREATE COPY PAGE
        public async Task<IActionResult> CreateCopy(int id)
        {
            //id = 102036;
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();
            

            var response = await client.GetAsync($"{apiBase}/api/incidents/getticketforcopy/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var data = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<IncidentCopyViewModel>(data);

            // Load Category dropdown
            var catResponse = await client.GetAsync($"{apiBase}/api/Category/GetAll");

            var categories =
                await catResponse.Content.ReadFromJsonAsync<List<DropdownDto>>();

            model.CategoryList = categories
    .Select(x => new SelectListItem
    {
        Value = x.Id.ToString(),
        Text = x.Name
    }).ToList();

            // Load ServiceType dropdown
            var typeResponse = await client.GetAsync($"{apiBase}/api/SubCategory/GetAll");
            var ServiceTypes =
                await typeResponse.Content.ReadFromJsonAsync<List<DropdownDto>>();

            model.ServiceTypeList = ServiceTypes
    .Select(x => new SelectListItem
    {
        Value = x.Id.ToString(),
        Text = x.Name
    }).ToList();

            // Load Service dropdown
            var serviceResponse = await client.GetAsync($"{apiBase}/api/Service/GetAll");
            var Services =
                await serviceResponse.Content.ReadFromJsonAsync<List<DropdownDto>>();

            model.ServiceList = Services
    .Select(x => new SelectListItem
    {
        Value = x.Id.ToString(),
        Text = x.Name
    }).ToList();


            return View(model);
        }


        // SUBMIT COPY
        [HttpPost]
        public async Task<IActionResult> CreateCopy(IncidentCopyViewModel model)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{apiBase}/api/incidents/createcopy", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                int newTicketId = Convert.ToInt32(result);

                return RedirectToAction("Manage", new { id = newTicketId });
            }

            return View(model);
        }


        public async Task<IActionResult> Reassign(int incidentId)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{apiBase}/api/IncidentReassign/{incidentId}");

            var json = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<IncidentReassignModel>(json);

            return View(model);
        }

    }
}
