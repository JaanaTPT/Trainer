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
    public interface ITrainingExerciseRepository : IBaseRepository<TrainingExercise>
    {
        IEnumerable Trainings { get; set; }

        IEnumerable Exercises { get; set; }

        Task<PagedResult<TrainingExercise>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null);

        Task Delete(int id);
    }
}
