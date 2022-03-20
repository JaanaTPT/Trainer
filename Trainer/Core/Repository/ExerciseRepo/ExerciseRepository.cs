using System.Collections.Generic;
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
    }
}