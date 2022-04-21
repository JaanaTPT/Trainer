using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.ExerciseRepo;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IMapper _objectMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IUnitOfWork unitOfWork, IMapper objectMapper)
        {
            _objectMapper = objectMapper;
            _unitOfWork = unitOfWork;
            _exerciseRepository = unitOfWork.ExerciseRepository;
        }

        public async Task<PagedResult<ExerciseModel>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null)
        {
            var exercises = await _exerciseRepository.GetPagedList(page, pageSize, searchString, sortOrder);

            return _objectMapper.Map<PagedResult<ExerciseModel>>(exercises);
        }

        public async Task<ExerciseModel> GetById(int id)
        {
            var exercise = await _exerciseRepository.GetById(id);

            if (exercise == null)
            {
                return null;
            }

            return _objectMapper.Map<ExerciseModel>(exercise);
        }

        public async Task<ExerciseEditModel> GetForEdit(int id)
        {
            var exercise = await _exerciseRepository.GetById(id);
            if (exercise == null)
            {
                return null;
            }

            var model = _objectMapper.Map<ExerciseEditModel>(exercise);

            return model;
        }

        public IEnumerable<Exercise> DropDownList()
        {
            return _exerciseRepository.DropDownList();
        }

        public async Task<OperationResponse> Save(ExerciseEditModel model)
        {
            var response = new OperationResponse();

            if (model == null)
            {
                return response.AddError("", "Model was null");
            }

            var exercise = new Exercise();

            if (model.ID != 0)
            {
                exercise = await _exerciseRepository.GetById(model.ID);
                if (exercise == null)
                {
                    return response.AddError("", "Cannot find exercise with id " + model.ID);
                }
            }

            _objectMapper.Map(model, exercise);

            if (!response.Success)
            {
                return response;
            }

            await _exerciseRepository.Save(exercise);
            await _unitOfWork.CommitAsync();

            return response;
        }

        public async Task<OperationResponse> Delete(ExerciseModel model)
        {

            var response = new OperationResponse();
            if (model == null)
            {
                return response.AddError("", "Model was null");
            }

            var exercise = await _exerciseRepository.GetById(model.ID);
            if (exercise == null)
            {
                return response.AddError("", "Cannot find exercise with id " + model.ID);
            }
            await _exerciseRepository.Delete(model.ID);
            await _unitOfWork.CommitAsync();

            return response;
        }
    }
}
