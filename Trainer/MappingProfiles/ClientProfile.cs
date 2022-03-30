using AutoMapper;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.MappingProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<PagedResult<Client>, PagedResult<ClientModel>>();
            CreateMap<Client, ClientModel>();
            CreateMap<Client, ClientEditModel>();

            CreateMap<ClientEditModel, Client>()
              .ForMember(m => m.ID, m => m.Ignore())
              .ForMember(m => m.TrainingExercises, m => m.Ignore())
              .ForMember(m => m.Trainings, m => m.Ignore());
        }
    }
}
