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
              .ForMember(c => c.ID, c => c.Ignore())
              .ForMember(c => c.TrainingExercises, c => c.Ignore())
              .ForMember(c => c.Trainings, c => c.Ignore());
        }
    }
}
