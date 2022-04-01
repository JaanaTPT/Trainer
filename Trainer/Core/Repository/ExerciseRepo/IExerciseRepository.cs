using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository.ExerciseRepo
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        IEnumerable<Exercise> DropDownList();
        Task<PagedResult<Exercise>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null);
        Task Delete(int id);
    }
}
