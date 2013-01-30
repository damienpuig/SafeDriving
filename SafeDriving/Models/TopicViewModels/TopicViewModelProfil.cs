using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using POCO;
using SafeDriving.AutoMapper;

namespace SafeDriving.Models.TopicViewModels
{
    public class TopicViewModelProfil : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "TopicViewModelProfil";
            }
        }

        protected override void Configure()
        {
            CreateMap<Topic, TopicViewModel>()
                .ForMember(x=>x.DateCreation, o=>o.ResolveUsing<DateTimeCustomResolver.DateTimeToStringResolver>().FromMember(z=>z.DateCreation))
                .ForMember(x=>x.Messages, o=>o.MapFrom(z=>z.Messages))
                .ForMember(x => x.NomCreateur, o => o.Ignore())
                .ForMember(x => x.MessageNewTopic, o => o.Ignore())
                .ForMember(x => x.IdCreateur, o => o.Ignore())
                ;
        }
    }
}