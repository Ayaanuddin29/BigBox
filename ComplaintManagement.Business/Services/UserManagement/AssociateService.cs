using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.UserManagement
{
    public class AssociateService : IAssociateService
    {
        private readonly IAssociateRepository _repository;

        public AssociateService(IAssociateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AssociateDto>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<AssociateDto> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task UpdateAsync(AssociateDto model)
            => await _repository.UpdateAsync(model);

        public async Task SyncAssociateByRolesAsync(
    string username,
    IEnumerable<string> selectedRoles)
        {
            var associateRoles = new List<string>
    {
        "Support 1",
        "ServiceDesk Staff"
    };

            bool requiresAssociate = selectedRoles
                .Any(r => associateRoles.Contains(r));

            var existing = await _repository.GetByUsernameAsync(username);

            if (requiresAssociate)
            {
                if (existing == null)
                {
                    var model = new AssociateDto
                    {
                        AssociateName = username,
                        AssociateName2 = username,
                        AssociateName3 = username,
                        Availability = true,
                        Active = true,
                        Username = username
                    };

                    await _repository.InsertAsync(model);
                }
                else
                {
                    await _repository.ReactivateAsync(username);
                }
            }
            else
            {
                if (existing != null)
                {
                    await _repository.DeactivateByUsernameAsync(username);
                }
            }
        }

    }

}
