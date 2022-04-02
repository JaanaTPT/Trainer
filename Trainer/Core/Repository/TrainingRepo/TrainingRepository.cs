using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        IEnumerable ITrainingRepository.Clients { get ; set; }

        public async Task<PagedResult<Training>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null)
        {
            IQueryable<Training> query = _context.Trainings.Include(s => s.Client)
                                          .Include(t => t.TrainingExercises)
                                          .ThenInclude(i => i.Exercise);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(i => i.Client.FirstName.Contains(searchString) ||
                                         i.Client.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_asc":
                    query = query.OrderBy(s => s.Date);
                    break;
                case "fullName_asc":
                    query = query.OrderBy(s => s.Client.FullName);
                    break;
                default:
                    query = query.OrderByDescending(s => s.Date);
                    break;
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        public IEnumerable<Training> DropDownList()
        {
            return _context.Trainings;
        }

        public override async Task<Training> GetById(int id)
        {
            return await _context.Trainings.Include(s => s.Client)
                                           .Include(t => t.TrainingExercises)
                                           .ThenInclude(i => i.Exercise)
                                           .Where(c => c.ID == id)
                                           .OrderBy(v => v.Date)
                                           .FirstOrDefaultAsync();
        }

        public async Task Delete(Training training)
        {
            _context.Trainings.Remove(training);
        }

        public async Task Delete(int id)
        {
            var training = await GetById(id);
            await Delete(training);
        }
    }
}