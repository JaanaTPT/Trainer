using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            //CreateMap<ClientEditModel, Client>()
            //  .ForMember(m => m.ID, m => m.Ignore());
        }
    }
}
