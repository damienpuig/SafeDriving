using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using POCO;
using SERVICES.ArticleSvc;
using Direction.AutoMapper;

namespace Direction.Models.ArticleViewModels
{
    public class ArticleViewModelProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ArticleViewModelProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<Article, ArticleViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(z => z.Id))
                .ForMember(x => x.ListeCategorie, o => o.Ignore())
                .ForMember(x => x.Contenu, o => o.MapFrom(x => x.Contenu))
                .ForMember(x => x.Date, o => o.ResolveUsing<DateTimeCustomResolver.DateTimeToStringResolver>().FromMember(src => src.Date))
                ;

            CreateMap<ArticleViewModel, CreateArticleCommand>()
                .ForMember(x => x.Date, o => o.Ignore())
                .ForMember(x => x.TypeArticleId, o => o.Ignore());

            CreateMap<ArticleViewModel, UpdateArticleCommand>()
                .ForMember(x => x.Date, o => o.Ignore())
                .ForMember(x => x.TypeArticleId, o => o.Ignore());
        }
    }
}