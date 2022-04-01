using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.ExerciseRepo;
using Trainer.Models;

namespace Trainer.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _exerciseRepository = unitOfWork.ExerciseRepository;
        }

        public async Task<List<Exercise>> List()
        {
            var exercises = await _exerciseRepository.List();

            return exercises.ToList();
        }

        public async Task<Exercise> GetById(int id)
        {
            var client = await _exerciseRepository.GetById(id);

            if (client == null)
            {
                return null;
            }

            return client;
        }

        public IEnumerable<Exercise> DropDownList()
        {
            return _exerciseRepository.DropDownList();
        }

        public async Task Save(Exercise exercise)
        {
            await _exerciseRepository.Save(exercise);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Exercise exercise)
        {
            _exerciseRepository.Delete(exercise);
            await _unitOfWork.CommitAsync();
        }
    }
}
