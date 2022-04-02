using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.TrainingRepo;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IMapper _objectMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainingRepository _trainingRepository;

        public TrainingService(IUnitOfWork unitOfWork, IMapper objectMapper)
        {
            _objectMapper = objectMapper;
            _unitOfWork = unitOfWork;
            _trainingRepository = unitOfWork.TrainingRepository;
        }

        public IEnumerable<Training> DropDownList()
        {
            return _trainingRepository.DropDownList();
        }

        public async Task<TrainingModel> GetById(int id)
        {
            var training = await _trainingRepository.GetById(id);

            if (training == null)
            {
                return null;
            }

            return _objectMapper.Map<TrainingModel>(training);
        }

        public async Task<TrainingEditModel> GetForEdit(int id)
        {
            var training = await _trainingRepository.GetById(id);
            if (training == null)
            {
                return null;
            }

            var model = _objectMapper.Map<TrainingEditModel>(training);

            return model;
        }

        public async Task<PagedResult<TrainingModel>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null)
        {
            var trainings = await _trainingRepository.GetPagedList(page, pageSize, searchString, sortOrder);

            return _objectMapper.Map<PagedResult<TrainingModel>>(trainings);
        }

        public async Task<OperationResponse> Save(TrainingEditModel model)
        {
            var response = new OperationResponse();

            if (model == null)
            {
                return response.AddError("", "Model was null");
            }

            var training = new Training();

            if (model.ID != 0)
            {
                training = await _trainingRepository.GetById(model.ID);
                if (training == null)
                {
                    return response.AddError("", "Cannot find client with id " + model.ID);
                }
            }

            _objectMapper.Map(model, training);

            if (!response.Success)
            {
                return response;
            }

            await _trainingRepository.Save(training);
            await _unitOfWork.CommitAsync();

            return response;
        }

        public async Task<OperationResponse> Delete(TrainingModel model)
        {

            var response = new OperationResponse();
            if (model == null)
            {
                return response.AddError("", "Model was null");
            }

            var training = await _trainingRepository.GetById(model.ID);
            if (training == null)
            {
                return response.AddError("", "Cannot find training with id " + model.ID);
            }
            await _trainingRepository.Delete(model.ID);
            await _unitOfWork.CommitAsync();

            return response;
        }
    }
}
