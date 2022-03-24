using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.Services
{
    public interface IClientService
    {
        Task<PagedResult<ClientModel>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null);
        Task<ClientModel> GetById(int id);
        IEnumerable<Client> DropDownList();
        Task<OperationResponse> Save(ClientModel model);
        Task<OperationResponse> Delete(ClientModel model);
    }
}
