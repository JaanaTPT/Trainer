using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository;

namespace Trainer.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TrainingContext _context;

        public IClientRepository Clients { get; private set; }

        public UnitOfWork(TrainingContext context,
                          IClientRepository clientRepository)
        {
            _context = context;

            Clients = clientRepository;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task BeginTransaction()
        {
            await Task.CompletedTask;
        }

        public async Task Rollback()
        {
            await Task.CompletedTask;
        }
    }
}
