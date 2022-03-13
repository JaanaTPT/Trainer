using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Services
{
    public interface IClientService
    {
        Task<PagedResult<Client>> GetPagedList(int page, int pageSize);
        Task<Client> GetById(int id);
        IEnumerable<Client> DropDownList();
        Task Save(Client client);
        Task Delete(Client client);
    }
}
