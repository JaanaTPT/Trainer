using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository.ClientRepo
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        private readonly TrainingContext _context;

        public ClientRepository(TrainingContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Client> DropDownList()
        {
            return  _context.Clients;
        }

        public async Task<Client> GetById(int id)
        {
            return await _context.Clients.Include(s => s.Trainings).FirstOrDefaultAsync(c => c.ID == id);       
        }

        public override async Task<PagedResult<Client>> GetPagedList(int page, int pageSize)
        {
            var clients = await _context.Clients.Include(s => s.Trainings).GetPagedAsync(page, pageSize);

            return clients;
        }

        public async Task Save(Client client)
        {
            if (client.ID == 0)
            {
                await _context.Clients.AddAsync(client);
            }
            else
            {
                _context.Clients.Update(client);
            }
        }

        public async Task Delete(Client client)
        {
            _context.Clients.Remove(client);
        }
    }
} 