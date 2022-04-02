using AutoMapper;
using System.Collections.Generic;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.MappingProfiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<PagedResult<Training>, PagedResult<TrainingModel>>();
            CreateMap<Training, TrainingModel>();
            CreateMap<Training, TrainingEditModel>();

            CreateMap<TrainingEditModel, Training>()
              .ForMember(m => m.ID, m => m.Ignore())
              .ForMember(m => m.Client, m => m.Ignore())
              .ForMember(m => m.TrainingExercises, m => m.Ignore());
        }
    }
}
