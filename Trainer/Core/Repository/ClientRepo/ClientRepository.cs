using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public override async Task<Client> GetById(int id)
        {
            return await _context.Clients
                                 .Include(c => c.Trainings)
                                 .FirstOrDefaultAsync(c => c.ID == id);       
        }

        public async Task<PagedResult<Client>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null)
        {
            IQueryable<Client> query = _context.Clients.Include(c => c.Trainings);

            if(!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(client => client.FirstName.Contains(searchString) ||
                                              client.LastName.Contains(searchString));       
            }

            switch (sortOrder)
            {
                case "lastName_asc":
                    query = query.OrderBy(c => c.LastName);
                    break;
                case "lastName_desc":
                    query = query.OrderByDescending(c => c.LastName);
                    break;
                case "firstName_desc":
                    query = query.OrderByDescending(c => c.FirstName);
                    break;
                default:
                    query = query.OrderBy(c => c.FirstName);
                    break;
            }

            return await query.GetPagedAsync(page, pageSize);
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

        public async Task Delete(int id)
        {
            var client = await GetById(id);
            await Delete(client);
        }
    }
} 