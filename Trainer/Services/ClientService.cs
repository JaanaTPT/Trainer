using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.ClientRepo;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientRepository _clientRepository;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _clientRepository = unitOfWork.ClientRepository;
        }


        public async Task<Client> GetById(int id)
        {
            var client = await _clientRepository.GetById(id);

            if (client == null)
            {
                return null;
            }

            return client;
        }

        public async Task<PagedResult<Client>> GetPagedList(int page, int pageSize)
        {
            var clients = await _clientRepository.GetPagedList(page, pageSize);

            return clients;
        }

        public IEnumerable<Client> DropDownList()
        {
            return _clientRepository.DropDownList();
        }

        public async Task Save(Client client)
        {
            await _clientRepository.Save(client);
            await _unitOfWork.CommitAsync(); 
        }

        public async Task Delete(Client client)
        {
            _clientRepository.Delete(client);
            await _unitOfWork.CommitAsync();
        }
    }
}
