using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.UtilisateurSvc
{
    public class ClientProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "ClientProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateClientCommand, Client>()
                .ForMember(Client => Client.Id, o => o.Ignore())
                .ForMember(Client=>Client.Avatar, o=>o.MapFrom(src => src.Avatar))
                .ForMember(Client => Client.Offres, o => o.Ignore())
                .ForMember(Client => Client.RoleId, o => o.Ignore())
                .ForMember(Client => Client.Role, o => o.Ignore())
                .ForMember(Client=>Client.Avatar, o=>o.MapFrom(z=>z.Avatar))
                .ForMember(x => x.IsCoded, o => o.Ignore())
                .ForMember(x => x.Utilisateurs_ClientSessions, o => o.Ignore())
                .ForMember(x => x.IsCoded, o => o.Ignore())
                ;

            CreateMap<UpdateClientCommand, Client>()
               .ForMember(Client => Client.Id, o => o.Ignore())
               .ForMember(Client => Client.Avatar, o => o.MapFrom( src => src.Avatar))
               .ForMember(Client => Client.Offres, o => o.Ignore())
               .ForMember(Client => Client.RoleId, o => o.Ignore())
               .ForMember(Client => Client.Role, o => o.Ignore())
               .ForMember(Client => Client.DateNaissance, o => o.MapFrom(src => src.DateNaissance))
               .ForMember(Client => Client.Password, o => o.Ignore())
               .ForMember(x => x.IsCoded, o => o.Ignore())
               .ForMember(x => x.Utilisateurs_ClientSessions, o => o.Ignore())
               .ForMember(x => x.IsCoded, o => o.Ignore())
               ;
        }
    }
}

