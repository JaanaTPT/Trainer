using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Services
{
    public interface ITrainingService
    {
        Task<IList<Training>> List(string search);
        IEnumerable<Training> DropDownList();
        Task<Training> GetById(int id);
        Task Save(Training training);
        Task Delete(Training training);
    }
}
