using ComplaintManagement.Business.Interfaces.Incident;
using ComplaintManagement.Data.Interfaces.Incident;
using ComplaintManagement.Util.Models.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.Incident
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<int> CreateIncidentAsync(IncidentCreateDto dto)
        {
            // You can add business logic here before saving
            // Example: set default values
            //dto.CreatedOn = DateTime.UtcNow;

            return await _incidentRepository.InsertIncidentAsync(dto);
        }

        public async Task<List<IncidentListDto>> GetIncidentListAsync()
        {
            return await _incidentRepository.GetIncidentListAsync();
        }

        public async Task<IncidentListDto> GetIncidentByIdAsync(string incId)
        {
            return await _incidentRepository.GetIncidentByIdAsync(incId);
        }

        public async Task<bool> UpdateIncidentAsync(IncidentUpdateDto dto)
        {
            // You can add business logic here before saving
            return await _incidentRepository.UpdateIncidentAsync(dto);
            //return false;
        }
        public async Task<int> CreateIncidentActivityAsync(IncidentActivityDto dto)
        {
            return await _incidentRepository.CreateIncidentActivityAsync(dto);
        }

        public async Task<List<ActivityReadDto>> GetActivitiesByIncIdAsync(string incId)
        {
            return await _incidentRepository.GetActivitiesByIncIdAsync(incId);
        }
        public async Task<int> CreateIncidentTasksync(IncidentTasksDto dto)
        {
            return await _incidentRepository.CreateIncidentTasksync(dto);
        }
        public async Task<List<IncidentTaskReadDto>> GetTasksByIncIdAsync(string incId)
        {
            return await _incidentRepository.GetTasksByIncIdAsync(incId);
        }

        public IncidentCopyViewModel GetTicketForCopy(int id)
        {
            return _incidentRepository.GetTicketForCopy(id);
        }

        public int CreateCopyTicket(IncidentCopyViewModel model)
        {
            return _incidentRepository.CreateCopyTicket(model);
        }

        public async Task ToggleMajorIncident(string incidentNumber)
        {
            await _incidentRepository.ToggleMajorIncident(incidentNumber);
        }

        public void UpdateState(string incidentId, string state)
        {
            _incidentRepository.UpdateState(incidentId, state);
        }

        public async Task<int> SaveResolution(ResolutionModel model)
        {
            return await _incidentRepository.SaveResolution(model);
        }

        public async Task<IEnumerable<ResolutionModel>> GetResolutions(int incid_no)
        {
            return await _incidentRepository.GetResolutions(incid_no);
        }

        public async Task<ResolutionModel> GetResolution(int id)
        {
            return await _incidentRepository.GetResolutionById(id);
        }
    }
}
