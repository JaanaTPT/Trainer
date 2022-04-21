using AutoMapper;
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
            CreateMap<Training, TrainingModel>()
                .ForMember(tm => tm.ClientName, tm => tm.MapFrom(t => t.Client.FullName));
            CreateMap<Training, TrainingEditModel>();

            CreateMap<TrainingEditModel, Training>()
              .ForMember(tm => tm.ID, tm => tm.Ignore())
              .ForMember(tm => tm.Client, tm => tm.Ignore())
              .ForMember(tm => tm.TrainingExercises, tm => tm.Ignore());
        }
    }
}
