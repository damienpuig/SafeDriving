using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.DateSvc
{
    public class DateProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "DateProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateDateCommand, Date>()
                .ForMember(Date => Date.Id, o => o.Ignore());

            CreateMap<UpdateDateCommand, Date>()
               .ForMember(Date => Date.Id, o => o.Ignore());
        }
    }
}

