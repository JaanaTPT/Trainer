using AutoMapper;
using System.Collections.Generic;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.MappingProfiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<List<Training>, List<TrainingModel>>();
            CreateMap<Training, TrainingModel>();
            CreateMap<Training, TrainingEditModel>();

            CreateMap<TrainingEditModel, Training>()
              .ForMember(m => m.ID, m => m.Ignore())
              .ForMember(m => m.TrainingExercises, m => m.Ignore());
        }
    }
}
