using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models.ViewModels;

namespace Trainer.Services
{
    public interface ITrainingExerciseService
    {
        Task<PagedResult<TrainingExerciseModel>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null);
        Task<TrainingExerciseModel> GetById(int id);
        Task<TrainingExerciseEditModel> GetForEdit(int id);
        Task<OperationResponse> Save(TrainingExerciseEditModel model);
        Task<OperationResponse> Delete(TrainingExerciseModel model);
        Task FillEditModel(TrainingExerciseEditModel model);

    }
}
