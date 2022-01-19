using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Core.Repository.TrainingExerciseRepo
{
    public class FakeTrainingExerciseRepository : ITrainingExerciseRepository
    {
        private List<TrainingExercise> _trainingExerciseList = new List<TrainingExercise>();

        DbSet<Exercise> ITrainingExerciseRepository.Exercises { get; set; }
        DbSet<Training> ITrainingExerciseRepository.Trainings { get; set; }

        public async Task Save(TrainingExercise trainingExercise)
        {
            if (trainingExercise.ID == 0)
            {
                _trainingExerciseList.Add(trainingExercise);
            }
        }

        public void Delete(TrainingExercise trainingExercise)
        {
            _trainingExerciseList.Remove(trainingExercise);
        }

        public async Task<TrainingExercise> GetById(int id)
        {
            return _trainingExerciseList.FirstOrDefault(trainingExercise => trainingExercise.ID == id);
        }

        public async Task<List<TrainingExercise>> List()
        {
            return _trainingExerciseList.ToList();
        }
    }
}
