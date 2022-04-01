using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.TrainingRepo;
using Trainer.Models;

namespace Trainer.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainingRepository _trainingRepository;

        public TrainingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _trainingRepository = unitOfWork.TrainingRepository;
        }

        public async Task<IList<Training>> List(string search)
        {
            var trainings = _trainingRepository.List(search);

            return await trainings;
        }

        public IEnumerable<Training> DropDownList()
        {
            return _trainingRepository.DropDownList();
        }

        public async Task<Training> GetById(int id)
        {
            var training = await _trainingRepository.GetById(id);

            if (training == null)
            {
                return null;
            }

            return training;
        }

        public async Task Save(Training training)
        {
            await _trainingRepository.Save(training);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Training training)
        {
            _trainingRepository.Delete(training);
            await _unitOfWork.CommitAsync();
        }
    }
}
