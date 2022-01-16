using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository.ExerciseRepo
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        private readonly TrainingContext _context;

        public ExerciseRepository(TrainingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Exercise> GetById(int id)
        {
            return await _context.Exercises.FirstOrDefaultAsync();

        }
    }
}
