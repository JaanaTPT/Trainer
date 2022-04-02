using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository.TrainingRepo
{
    public interface ITrainingRepository : IBaseRepository<Training>
    {
        IEnumerable Clients { get; set; }
        Task<PagedResult<Training>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null);

        IEnumerable<Training> DropDownList();

        Task Delete(int id);
    }
}
