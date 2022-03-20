using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository.ClientRepo
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        IEnumerable<Client> DropDownList();
        Task<PagedResult<Client>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null);
    }
}