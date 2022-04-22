using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Exercise> DropDownList()
        {
            return _context.Exercises;
        }

        public override async Task<Exercise> GetById(int id)
        {
            return await _context.Exercises
                                 .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task<PagedResult<Exercise>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null)
        {
            IQueryable<Exercise> query = _context.Exercises;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(e => e.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(e => e.Title);
                    break;
                case "muscle_asc":
                    query = query.OrderBy(e => e.MuscleGroup);
                    break;
                case "muscle_desc":
                    query = query.OrderByDescending(e => e.MuscleGroup);
                    break;
                default:
                    query = query.OrderBy(e => e.Title);
                    break;
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        public async Task Save(Exercise exercise)
        {
            if (exercise.ID == 0)
            {
                await _context.Exercises.AddAsync(exercise);
            }
            else
            {
                _context.Exercises.Update(exercise);
            }
        }

        public async Task Delete(Exercise exercise)
        {
            _context.Exercises.Remove(exercise);
        }

        public async Task Delete(int id)
        {
            var exercise = await GetById(id);
            await Delete(exercise);
        }
    }
}