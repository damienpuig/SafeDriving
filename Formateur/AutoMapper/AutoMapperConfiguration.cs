using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SERVICES.ArticleSvc;
using Formateur.Models.ArticleViewModels;
using Formateur.Models.CategorieViewModels;
using Formateur.Models.TopicViewModels;
using Formateur.Models.MessageViewModels;
using Formateur.Models.AccountViewModels;

namespace Formateur.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ArticleProfile>();
                cfg.AddProfile<ArticleViewModelProfil>();
                cfg.AddProfile<CategorieViewModelProfil>();
                cfg.AddProfile<TopicViewModelProfil>();
                cfg.AddProfile<MessageViewModelProfil>();
                cfg.AddProfile<AccountViewModelProfil>();
            });



            Mapper.AssertConfigurationIsValid();
        }
    }
}