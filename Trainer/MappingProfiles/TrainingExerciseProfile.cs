using AutoMapper;
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
                .ForMember(tem => tem.ExerciseName, tem => tem.MapFrom(te => te.Exercise.Title))
                .ForMember(tem => tem.ClientName, tem => tem.MapFrom(te => te.Training.Client.FullName))
                .ForMember(tem => tem.TrainingDate, tem => tem.MapFrom(te => te.Training.Date));
            CreateMap<TrainingExercise, TrainingExerciseEditModel>();

            CreateMap<TrainingExerciseEditModel, TrainingExercise>()
              .ForMember(te => te.ID, te => te.Ignore())
              .ForMember(te => te.Training, te => te.Ignore())
              .ForMember(te => te.Exercise, te => te.Ignore());
        }
    }
}
