using AutoMapper;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.MappingProfiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<PagedResult<Exercise>, PagedResult<ExerciseModel>>();
            CreateMap<Exercise, ExerciseModel>();
            CreateMap<Exercise, ExerciseEditModel>();

            CreateMap<ExerciseEditModel, Exercise>()
              .ForMember(e => e.ID, e => e.Ignore())
              .ForMember(e => e.TrainingExercises, e => e.Ignore());
        }
    }
}
