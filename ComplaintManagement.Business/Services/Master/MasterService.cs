using ComplaintManagement.Business.Interfaces.Master;
using ComplaintManagement.Data.Interfaces.Master;
using ComplaintManagement.Util.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.Master
{
    public class MasterService:IMasterService
    {
        private readonly IMasterRepository _repo;

        public MasterService(IMasterRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MasterModel>> GetAllAsync(string tableName)
        {
            return await _repo.GetAllAsync(tableName);
        }

        public async Task<int> SaveAsync(MasterModel model)
        {
            if (model.Id == 0)
                return await _repo.InsertAsync(model);
            else
                return await _repo.UpdateAsync(model);
        }

        public async Task<int> DeleteAsync(int id, string tableName)
        {
            return await _repo.DeleteAsync(id, tableName);
        }

        public async Task<IEnumerable<AssociateModel>> GetAssociates()
        {
            return await _repo.GetAssociates();
        }

        public async Task<IEnumerable<AssociateGroupModel>> GetAssociateGroups()
        {
            return await _repo.GetAssociateGroups();
        }


    }
}
