using AutoMapper;
using System.Collections.Generic;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.MappingProfiles
{
    public class TrainingExerciseProfile : Profile
    {
        public TrainingExerciseProfile()
        {
            CreateMap<List<TrainingExercise>, List<TrainingExerciseModel>>();
            CreateMap<TrainingExercise, TrainingExerciseModel>();
            CreateMap<TrainingExercise, TrainingExerciseEditModel>();

            CreateMap<TrainingExerciseEditModel, TrainingExercise>()
              .ForMember(m => m.ID, m => m.Ignore());
        }
    }
}
