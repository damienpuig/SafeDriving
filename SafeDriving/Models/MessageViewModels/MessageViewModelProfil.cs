using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using POCO;
using SafeDriving.Models.TopicViewModels;
using SafeDriving.AutoMapper;

namespace SafeDriving.Models.MessageViewModels
{
    public class MessageViewModelProfil : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "MessageViewModelProfil";
            }
        }

        protected override void Configure()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(x => x.DateCreation, o => o.ResolveUsing<DateTimeCustomResolver.DateTimeToStringResolver>().FromMember(z => z.DateCreation))
                .ForMember(x=>x.NomCreateur, o=>o.Ignore())
                .ForMember(x => x.IdCreateur, o => o.Ignore())
                ;
        }
    }
}