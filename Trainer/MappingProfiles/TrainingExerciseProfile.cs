using AutoMapper;
using System.Collections.Generic;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.MappingProfiles
{
    public class TrainingExerciseProfile : Profile
    {
        public TrainingExerciseProfile()
        {
            CreateMap<PagedResult<TrainingExercise>, PagedResult<TrainingExerciseModel>>();
            CreateMap<TrainingExercise, TrainingExerciseModel>()
                .ForMember(tem => tem.ExerciseName, m => m.MapFrom(e => e.Exercise.Title))
                .ForMember(tem => tem.ClientName, m => m.MapFrom(e => e.Training.Client.FullName))
                .ForMember(tem => tem.TrainingDate, m => m.MapFrom(e => e.Training.Date));
            CreateMap<TrainingExercise, TrainingExerciseEditModel>();

            CreateMap<TrainingExerciseEditModel, TrainingExercise>()
              .ForMember(m => m.ID, m => m.Ignore())
              .ForMember(m => m.Training, m => m.Ignore())
              .ForMember(m => m.Exercise, m => m.Ignore());
        }
    }
}
