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

        public async Task<IList<Training>> List(string search)
        {
            var query = _context.Trainings.Include(s => s.Client)
                                          .Include(t => t.TrainingExercises)
                                          .ThenInclude(i => i.Exercise)
                                          .Select(s => s);

            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(i => i.Client.FirstName.Contains(search) ||
                                         i.Client.LastName.Contains(search));
            }

            return await query.ToListAsync();
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
    }
}