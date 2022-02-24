using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Core.Repository.TrainingExerciseRepo
{
    public interface ITrainingExerciseRepository : IBaseRepository<TrainingExercise>
    {
        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Training> Trainings { get; set; }

        Task<IList<TrainingExercise>> List(string search);
    }
}
