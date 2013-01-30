using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.TypeSessionSvc
{
    public class TypeSessionProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "TypeSessionProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateTypeSessionCommand, TypeSession>()
                .ForMember(TypeSession => TypeSession.Id, o => o.Ignore());

            CreateMap<UpdateTypeSessionCommand, TypeSession>()
               .ForMember(TypeSession => TypeSession.Id, o => o.Ignore());
        }
    }
}

