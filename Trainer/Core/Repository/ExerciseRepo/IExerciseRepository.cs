using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Core.Repository.ExerciseRepo
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        IEnumerable<Exercise> DropDownList();
    }
}
