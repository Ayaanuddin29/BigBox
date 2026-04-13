using ComplaintManagement.Util.Models.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Incident
{
    public interface IIncidentRepository
    {
        Task<int> InsertIncidentAsync(IncidentCreateDto dto);
        Task<List<IncidentListDto>> GetIncidentListAsync();
        Task<IncidentListDto> GetIncidentByIdAsync(string incId);
        Task<bool> UpdateIncidentAsync(IncidentUpdateDto dto);
        Task<int> CreateIncidentActivityAsync(IncidentActivityDto dto);

        Task<List<ActivityReadDto>> GetActivitiesByIncIdAsync(string incId);

        Task<int> CreateIncidentTasksync(IncidentTasksDto dto);
        Task<List<IncidentTaskReadDto>> GetTasksByIncIdAsync(string incId);
        IncidentCopyViewModel GetTicketForCopy(int ticketId);
        int CreateCopyTicket(IncidentCopyViewModel model);

        Task ToggleMajorIncident(string incidentNumber);

        void UpdateState(string incidentId, string state);

        Task<int> SaveResolution(ResolutionModel model);

        Task SaveAttachment(ResolutionAttachment file);

        Task<IEnumerable<ResolutionModel>> GetResolutions(int incid_no);

        Task<ResolutionModel> GetResolutionById(int id);
    }
}
