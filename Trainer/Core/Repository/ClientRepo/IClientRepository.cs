using Trainer.Models;
using System.Collections.Generic;

namespace Trainer.Core.Repository.ClientRepo
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        IEnumerable<Client> DropDownList();
    }
}