using System;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.ClientRepo;
using Trainer.Core.Repository.ExerciseRepo;
using Trainer.Core.Repository.TrainingExerciseRepo;
using Trainer.Core.Repository.TrainingRepo;

namespace Trainer.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TrainingContext _context;

        public IClientRepository ClientRepository { get; private set; }

        public IExerciseRepository ExerciseRepository { get; private set; }

        public ITrainingRepository TrainingRepository { get; private set; }

        public ITrainingExerciseRepository TrainingExerciseRepository { get; private set; }

        public UnitOfWork(TrainingContext context,
                          IClientRepository clientRepository,
                          IExerciseRepository exerciseRepository,
                          ITrainingRepository trainingRepository,
                          ITrainingExerciseRepository trainingExerciseRepository)
        {
            _context = context;

            ClientRepository = clientRepository;
            ExerciseRepository = exerciseRepository;
            TrainingRepository = trainingRepository;
            TrainingExerciseRepository = trainingExerciseRepository;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task BeginTransaction()
        {
            await Task.CompletedTask;
        }

        public async Task Rollback()
        {
            await Task.CompletedTask;
        }
    }
}