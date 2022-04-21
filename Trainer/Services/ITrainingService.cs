using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.Services
{
    public interface ITrainingService
    {
        Task<PagedResult<TrainingModel>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null);
        IEnumerable<Training> DropDownList();
        Task<TrainingModel> GetById(int id);
        Task<TrainingEditModel> GetForEdit(int id);
        Task<OperationResponse> Save(TrainingEditModel model);
        Task<OperationResponse> Delete(TrainingModel model);
        Task FillEditModel(TrainingEditModel model);
    }
}
