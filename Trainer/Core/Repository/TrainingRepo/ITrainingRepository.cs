using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Core.Repository.TrainingRepo
{
    public interface ITrainingRepository : IBaseRepository<Training>
    {
        IEnumerable Clients { get; set; }
    }
}
