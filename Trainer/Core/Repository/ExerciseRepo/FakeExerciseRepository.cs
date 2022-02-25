using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Data;
using Trainer.Models;

namespace Trainer.Core.Repository.ExerciseRepo
{
    public class FakeExerciseRepository : IExerciseRepository
    {
        private List<Exercise> _exerciseList = new List<Exercise>();

        public async Task Save(Exercise exercise)
        {
            if (exercise.ID == 0)
            {
                _exerciseList.Add(exercise);
            }
        }

        public void Delete(Exercise exercise)
        {
            _exerciseList.Remove(exercise);
        }

        public async Task<Exercise> GetById(int id)
        {
            return _exerciseList.FirstOrDefault(exercise => exercise.ID == id);
        }

        public async Task<List<Exercise>> List()
        {
            return _exerciseList.ToList();
        }

        public IEnumerable<Exercise> DropDownList()
        {
            return _exerciseList;
        }

        public Task<PagedResult<Exercise>> GetPagedList(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
