using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Core.Repository.TrainingRepo
{
    public class FakeTrainingRepository : ITrainingRepository
    {
        private List<Training> _trainingList = new List<Training>();

        IEnumerable ITrainingRepository.Clients { get; set; }

        public async Task Save(Training training)
        {
            if (training.ID == 0)
            {
                _trainingList.Add(training);
            }
        }

        public void Delete(Training training)
        {
            _trainingList.Remove(training);
        }

        public async Task<Training> GetById(int id)
        {
            return _trainingList.FirstOrDefault(training => training.ID == id);
        }

        public async Task<List<Training>> List()
        {
            return _trainingList.ToList();
        }

    }
}
