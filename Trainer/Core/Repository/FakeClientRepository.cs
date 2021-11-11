using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Core.Repository
{
    public class FakeClientRepository : IClientRepository
    {
        private List<Client> _clientList = new List<Client>();

        public async Task Save(Client client)
        {
            if (client.ID == 0)
            {
                _clientList.Add(client);
            }
        }

        public void Delete(Client client)
        {
            _clientList.Remove(client);
        }

        public async Task<Client> GetById(int id)
        {
            return _clientList.FirstOrDefault(client => client.ID == id);
        }

        public async Task<List<Client>> List()
        {
            return _clientList.ToList();
        }

    }
}
