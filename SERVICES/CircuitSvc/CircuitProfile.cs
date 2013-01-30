using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.CircuitSvc
{
    public class CircuitProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "CircuitProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateCircuitCommand, Circuit>()
                .ForMember(Circuit => Circuit.Id, o => o.Ignore());

            CreateMap<UpdateCircuitCommand, Circuit>()
               .ForMember(Circuit => Circuit.Id, o => o.Ignore());
        }
    }
}

