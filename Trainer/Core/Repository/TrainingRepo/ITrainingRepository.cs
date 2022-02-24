using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Core.Repository.TrainingRepo
{
    public interface ITrainingRepository : IBaseRepository<Training>
    {
        IEnumerable Clients { get; set; }
        Task<IList<Training>> List(string search);

        IEnumerable<Training> DropDownList();
    }
}
