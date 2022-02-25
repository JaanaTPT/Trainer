using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;
using Microsoft.EntityFrameworkCore;

namespace Trainer.Core.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly TrainingContext _context;

        public BaseRepository(TrainingContext context)
        {
            _context = context;
        }

        public async Task Save(T instance)
        {
            if (instance.ID == 0)
            {
                await _context.Set<T>().AddAsync(instance);
            }
            else
            {
                _context.Set<T>().Update(instance);
            }
        }

        public void Delete(T instance)
        {
            _context.Set<T>().Remove(instance);
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> List()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<PagedResult<T>> GetPagedList(int page, int pageSize)
        {
            return await _context.Set<T>().GetPagedAsync(page, pageSize);
        }
    }
}