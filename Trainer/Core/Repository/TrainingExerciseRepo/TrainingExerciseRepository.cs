using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

        IEnumerable ITrainingExerciseRepository.Trainings { get; set; }
        IEnumerable ITrainingExerciseRepository.Exercises { get; set; }

        public async Task<PagedResult<TrainingExercise>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null)
        {
            IQueryable<TrainingExercise> query = _context.TrainingExercises
                                                .Include(te => te.Exercise)
                                                .Include(te => te.Training)
                                                .ThenInclude(t => t.Client);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(te => te.Training.Client.FirstName.Contains(searchString) ||
                                         te.Training.Client.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "clientName_asc":
                    query = query.OrderBy(te => (te.Training.Client.FirstName + " " + te.Training.Client.LastName))
                                 .ThenByDescending(te => te.Training.Date);
                    break;
                case "clientName_desc":
                    query = query.OrderByDescending(te => (te.Training.Client.FirstName + " " + te.Training.Client.LastName))
                                  .ThenByDescending(te => te.Training.Date);
                    break;
                case "date_asc":
                    query = query.OrderBy(te => te.Training.Date);
                    break;
                default:
                    query = query.OrderByDescending(te => te.Training.Date)
                                 .ThenBy(te => (te.Training.Client.FirstName + " " + te.Training.Client.LastName));
                    break;
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        public override async Task<TrainingExercise> GetById(int id)
        {
            return await _context.TrainingExercises.Include(te => te.Exercise)
                                                    .Include(te => te.Training)
                                                    .ThenInclude(t => t.Client)
                                                    .FirstOrDefaultAsync(te => te.ID == id);
        }

        public async Task Delete(TrainingExercise trainingExercise)
        {
            _context.TrainingExercises.Remove(trainingExercise);
        }

        public async Task Delete(int id)
        {
            var trainingExercises = await GetById(id);
            await Delete(trainingExercises);
        }

    }
}
