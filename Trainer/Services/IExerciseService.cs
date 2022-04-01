using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.Services
{
    public interface IExerciseService
    {
        IEnumerable<Exercise> DropDownList();
        Task<ExerciseModel> GetById(int id);
        Task<ExerciseEditModel> GetForEdit(int id);
        Task<PagedResult<ExerciseModel>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null);
        Task<OperationResponse> Save(ExerciseEditModel model);
        Task<OperationResponse> Delete(ExerciseModel model);
    }
}
