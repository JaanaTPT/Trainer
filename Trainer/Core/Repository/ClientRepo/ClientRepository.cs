﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Client> GetById(int id)
        {
           return await _context.Clients.Include(s => s.Trainings).FirstOrDefaultAsync(c => c.ID == id);
           
        }
    }
} 