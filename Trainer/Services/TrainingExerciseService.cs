using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.TrainingExerciseRepo;
using Trainer.Models;

namespace Trainer.Services
{
    public class TrainingExerciseService : ITrainingExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainingExerciseRepository _trainingExerciseRepository;

        public TrainingExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _trainingExerciseRepository = unitOfWork.TrainingExerciseRepository;
        }

        public async Task<IList<TrainingExercise>> List(string search)
        {
            var trainingExercises = _trainingExerciseRepository.List(search);
            return await trainingExercises;
        }

         public async Task<TrainingExercise> GetById(int id)
         {
            var trainingExercise = await _trainingExerciseRepository.GetById(id);

            if (trainingExercise == null)
            {
                return null;
            }

            return trainingExercise;
        }

        public async Task Save(TrainingExercise trainingExercise)
        {
            await _trainingExerciseRepository.Save(trainingExercise);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(TrainingExercise trainingExercise)
        {
            _trainingExerciseRepository.Delete(trainingExercise);
            await _unitOfWork.CommitAsync();
        }
    }
}
