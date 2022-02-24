using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IEnumerable<Exercise> DropDownList()
        {
            return _context.Exercises;
        }

        public async Task<Exercise> GetById(int id)
        {
            return await _context.Exercises.FirstOrDefaultAsync(c => c.ID == id);

        }
    }
}
