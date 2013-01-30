using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.ArticleSvc
{
    public class ArticleProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "ArticleProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateArticleCommand, Article>()
                .ForMember(article => article.Id, o => o.Ignore())
                .ForMember(article => article.EmployeId, o => o.Ignore())
                .ForMember(article => article.Employe, o => o.Ignore())
                .ForMember(article => article.Photo, o => o.Ignore())
                .ForMember(article => article.TypeArticleId, o => o.Ignore())
                .ForMember(article => article.Utilisateurs_Employe, o => o.Ignore())
                .ForMember(article => article.TypeArticle, o => o.Ignore());

            CreateMap<UpdateArticleCommand, Article>()
               .ForMember(article => article.Id, o => o.Ignore())
               .ForMember(article => article.EmployeId, o => o.Ignore())
               .ForMember(article => article.Employe, o => o.Ignore())
                .ForMember(article => article.Utilisateurs_Employe, o => o.Ignore())
               .ForMember(article => article.Photo, o => o.Ignore())
               .ForMember(article => article.TypeArticleId, o => o.Ignore())
               .ForMember(article => article.TypeArticle, o => o.Ignore());


        }
    }
}

