using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository.TrainingRepo
{
    public class TrainingRepository : BaseRepository<Training>, ITrainingRepository
    {

        private readonly TrainingContext _context;

        public TrainingRepository(TrainingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Training> GetById(int id)
        {
            return await _context.Trainings.Include(s => s.Client).Include(t =>t.TrainingExercises).ThenInclude(i => i.Exercise).OrderBy(v => v.Date).FirstOrDefaultAsync(c => c.ID == id);

        }
    }
}
