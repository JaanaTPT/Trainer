using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository
{
    public interface IBaseRepository<T>
    {
        Task Save(T instance);
        void Delete(T instance);
        Task<T> GetById(int id);
        Task<List<T>> List();
        Task<PagedResult<T>> GetPagedList(int page, int pageSize);
    }
}