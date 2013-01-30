using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using POCO;
using SERVICES.ParcoursSvc;
using SafeDriving.AutoMapper;

namespace SafeDriving.Models.ParcoursViewModels
{
    public class ParcoursViewModelProfile: Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ParcoursViewModelProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<ParcoursDto, ParcoursViewModel>()
                .ForMember(p => p.GraphDictionnary, o => o.MapFrom(src => src.GraphDictionnary))
                ;
        }
    }
}