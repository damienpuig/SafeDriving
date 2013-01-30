using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.ForfaitReferenceSvc
{
    public class ForfaitReferenceProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "ForfaitReferenceProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateForfaitReferenceCommand, ForfaitReference>()
                .ForMember(ForfaitReference => ForfaitReference.Id, o => o.Ignore());

            CreateMap<UpdateForfaitReferenceCommand, ForfaitReference>()
               .ForMember(ForfaitReference => ForfaitReference.Id, o => o.Ignore());
        }
    }
}

