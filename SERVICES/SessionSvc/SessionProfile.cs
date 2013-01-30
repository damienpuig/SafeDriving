using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.SessionSvc
{
    public class SessionProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "SessionProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateSessionCommand, Session>()
                .ForMember(Session => Session.Id, o => o.Ignore())
                .ForMember(Session => Session.Utilisateurs_ClientSessions, o => o.Ignore())
                .ForMember(Session => Session.Vehicules, o => o.Ignore())
                .ForMember(Session => Session.EtatSession, o => o.Ignore())
                .ForMember(Session => Session.Circuit, o => o.Ignore())
                .ForMember(Session => Session.Employe, o => o.Ignore())
                .ForMember(Session => Session.TypeSession, o => o.Ignore())
                ;

        }
    }
}

