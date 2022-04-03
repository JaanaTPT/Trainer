using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.Repository.TrainingExerciseRepo;
using Trainer.Models;

namespace Trainer.Services
{
    public interface ITrainingExerciseService
    {
        Task<IList<TrainingExercise>> List(string search);
        Task<TrainingExercise> GetById(int id);
        Task Save(TrainingExercise trainingExercise);
        Task Delete(TrainingExercise trainingExercise);

    }
}
