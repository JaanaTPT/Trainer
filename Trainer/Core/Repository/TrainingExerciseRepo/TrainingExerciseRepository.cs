using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository.TrainingExerciseRepo
{
    public class TrainingExerciseRepository : BaseRepository<TrainingExercise>, ITrainingExerciseRepository
    {
        private readonly TrainingContext _context;

        public TrainingExerciseRepository(TrainingContext context) : base(context)
        {
            _context = context;
        }

        DbSet<Exercise> ITrainingExerciseRepository.Exercises { get; set; }
        DbSet<Training> ITrainingExerciseRepository.Trainings { get; set; }

        public async Task<TrainingExercise> GetById(int id)
        {
            return await _context.TrainingExercises.Include(s => s.Exercise).Include(t => t.Training).ThenInclude(t => t.Client).FirstOrDefaultAsync(c => c.ID == id);

        }

    }
}
