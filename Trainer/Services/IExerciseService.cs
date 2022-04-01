using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Services
{
    public interface IExerciseService
    {
        IEnumerable<Exercise> DropDownList();
        Task<List<Exercise>> List();
        Task<Exercise> GetById(int id);
        Task Save(Exercise exercise);
        Task Delete(Exercise exercise);
    }
}
