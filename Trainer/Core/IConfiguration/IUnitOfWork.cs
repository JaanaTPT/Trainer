using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.Repository;

namespace Trainer.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }

        Task BeginTransaction();

        Task CommitAsync();

        Task Rollback();
    }
}
