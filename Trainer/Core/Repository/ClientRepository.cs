using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        private readonly TrainingContext _context;

        public ClientRepository(TrainingContext context) : base(context)
        {
            _context = context;
        }
    }
}