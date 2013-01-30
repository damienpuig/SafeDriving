using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using POCO;

namespace Direction.Models.CategorieViewModels
{
    public class CategorieViewModelProfil : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "CategorieViewModelProfil";
            }
        }

        protected override void Configure()
        {
            CreateMap<Categorie, CategorieViewModel>()
                .ForMember(x=>x.ListeTopic, o=>o.MapFrom(z=>z.Topics))
                .ForMember(x => x.NomCreateur, o => o.Ignore())
                .ForMember(x => x.TitreNewTopic, o => o.Ignore())
                .ForMember(x => x.MessageNewTopic, o => o.Ignore())
                .ForMember(x => x.TitreNewCategorie, o => o.Ignore())
                ;
        }
    }
}