using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.ExerciseRepo;
using Trainer.Core.Repository.TrainingExerciseRepo;
using Trainer.Core.Repository.TrainingRepo;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.Services
{
    public class TrainingExerciseService : ITrainingExerciseService
    {
        private readonly IMapper _objectMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainingExerciseRepository _trainingExerciseRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public TrainingExerciseService(IUnitOfWork unitOfWork, IMapper objectMapper)
        {
            _objectMapper = objectMapper;
            _unitOfWork = unitOfWork;
            _trainingExerciseRepository = unitOfWork.TrainingExerciseRepository;
            _trainingRepository = unitOfWork.TrainingRepository;
            _exerciseRepository = unitOfWork.ExerciseRepository;
        }

        public async Task<PagedResult<TrainingExerciseModel>> GetPagedList(int page, int pageSize, string searchString = null, string sortOrder = null)
        {
            var trainingExercises = await _trainingExerciseRepository.GetPagedList(page, pageSize, searchString, sortOrder);

            return _objectMapper.Map<PagedResult<TrainingExerciseModel>>(trainingExercises);
        }

        public async Task<TrainingExerciseModel> GetById(int id)
         {
            var trainingExercise = await _trainingExerciseRepository.GetById(id);

            if (trainingExercise == null)
            {
                return null;
            }

            return _objectMapper.Map<TrainingExerciseModel>(trainingExercise);
        }

        public async Task<TrainingExerciseEditModel> GetForEdit(int id)
        {
            var trainingExercise = await _trainingExerciseRepository.GetById(id);
            if (trainingExercise == null)
            {
                return null;
            }

            var model = _objectMapper.Map<TrainingExerciseEditModel>(trainingExercise);

            await FillEditModel(model);

            return model;
        }

        public async Task<OperationResponse> Save(TrainingExerciseEditModel model)
        {
            var response = new OperationResponse();

            if (model == null)
            {
                return response.AddError("", "Model was null");
            }

            var trainingExercise = new TrainingExercise();

            if (model.ID != 0)
            {
                trainingExercise = await _trainingExerciseRepository.GetById(model.ID);
                if (trainingExercise == null)
                {
                    return response.AddError("", "Cannot find training exercise with id " + model.ID);
                }
            }

            _objectMapper.Map(model, trainingExercise);

            trainingExercise.Training = await _trainingRepository.GetById(model.TrainingID);
            if (trainingExercise.Training == null)
            {
                response.AddError("TrainingID", "Cannot find training with id " + model.ID);
            }

            trainingExercise.Exercise = await _exerciseRepository.GetById(model.ExerciseID);
            if (trainingExercise.Exercise == null)
            {
                response.AddError("ExerciseID", "Cannot find exercise with id " + model.ID);
            }

            if (!response.Success)
            {
                return response;
            }

            await _trainingExerciseRepository.Save(trainingExercise);
            await _unitOfWork.CommitAsync();

            return response;
        }

        public async Task<OperationResponse> Delete(TrainingExerciseModel model)
        {

            var response = new OperationResponse();
            if (model == null)
            {
                return response.AddError("", "Model was null");
            }

            var trainingExercise = await _trainingExerciseRepository.GetById(model.ID);
            if (trainingExercise == null)
            {
                return response.AddError("", "Cannot find training exercise with id " + model.ID);
            }
            await _trainingExerciseRepository.Delete(model.ID);
            await _unitOfWork.CommitAsync();

            return response;
        }

        public async Task FillEditModel(TrainingExerciseEditModel model)
        {
            var trainings = await _trainingRepository.GetPagedList(1, 100);
            var exercises = await _exerciseRepository.GetPagedList(1, 100);

            model.Trainings = trainings.Results
                                   .OrderBy(m => m.Date)
                                   .Select(m => new SelectListItem
                                   {
                                       Text = m.Date.ToString(),
                                       Value = m.ID.ToString(),
                                       Selected = model.TrainingID == m.ID
                                   })
                                  .ToList();
            model.Exercises = exercises.Results
                                  .OrderBy(m => m.Title)
                                  .Select(m => new SelectListItem
                                  {
                                      Text = m.Title,
                                      Value = m.ID.ToString(),
                                      Selected = model.ExerciseID == m.ID
                                  })
                                 .ToList();
        }
    }
}
