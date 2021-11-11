using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;

namespace Trainer.Core.Repository
{
    public interface IBaseRepository<T>
    {
        Task Save(T client);
        void Delete(T client);
        Task<T> GetById(int id);
        Task<List<T>> List();
    }
}