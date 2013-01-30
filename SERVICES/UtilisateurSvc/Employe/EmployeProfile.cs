using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.UtilisateurSvc
{
    public class EmployeProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "EmployeProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateClientCommand, Employe>()
               .ForMember(Client => Client.Id, o => o.Ignore())
               .ForMember(Client => Client.Avatar, o => o.MapFrom(src => src.Avatar))
               .ForMember(Client => Client.Articles, o => o.Ignore())
               .ForMember(Client => Client.Articles_1, o => o.Ignore())
               .ForMember(Client => Client.RoleId, o => o.Ignore())
               .ForMember(Client => Client.Role, o => o.Ignore())
               .ForMember(Client => Client.Avatar, o => o.MapFrom(z => z.Avatar))
               .ForMember(x => x.Sessions, o => o.Ignore())
               ;

            CreateMap<UpdateClientCommand, Employe>()
               .ForMember(Client => Client.Id, o => o.Ignore())
               .ForMember(Client => Client.Avatar, o => o.MapFrom(src => src.Avatar))
               .ForMember(Client => Client.Articles, o => o.Ignore())
               .ForMember(Client => Client.Articles_1, o => o.Ignore())
               .ForMember(Client => Client.RoleId, o => o.Ignore())
               .ForMember(Client => Client.Role, o => o.Ignore())
               .ForMember(Client => Client.Avatar, o => o.MapFrom(z => z.Avatar))
               .ForMember(x => x.Sessions, o => o.Ignore())
               .ForMember(Client => Client.Password, o => o.Ignore())
               ;
        }
    }
}

