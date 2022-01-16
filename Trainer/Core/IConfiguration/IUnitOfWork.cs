using System.Threading.Tasks;
using Trainer.Core.Repository.ClientRepo;
using Trainer.Core.Repository.ExerciseRepo;
using Trainer.Core.Repository.TrainingExerciseRepo;
using Trainer.Core.Repository.TrainingRepo;

namespace Trainer.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get; }

        IExerciseRepository ExerciseRepository { get; }

        ITrainingRepository TrainingRepository { get; }

        ITrainingExerciseRepository TrainingExerciseRepository { get; }

        Task BeginTransaction();

        Task CommitAsync();

        Task Rollback();
    }
}
