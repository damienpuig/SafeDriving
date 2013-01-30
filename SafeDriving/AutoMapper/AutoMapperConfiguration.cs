using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SafeDriving.Models.ArticleViewModels;
using SERVICES.ArticleSvc;
using SafeDriving.Models;
using SERVICES.UtilisateurSvc;
using SafeDriving.Models.CategorieViewModels;
using SafeDriving.Models.TopicViewModels;
using SafeDriving.Models.MessageViewModels;
using SafeDriving.Models.ContactViewModel;
using SafeDriving.Models.ParcoursViewModels;

namespace SafeDriving.AutoMapper
{
        public class AutoMapperConfiguration
        {
            public static void Configure()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<ArticleProfile>();
                    cfg.AddProfile<ArticleViewModelProfile>();
                    cfg.AddProfile<AccountModelsProfils>();
                    cfg.AddProfile<ClientProfile>();
                    cfg.AddProfile<CategorieViewModelProfil>();
                    cfg.AddProfile<TopicViewModelProfil>();
                    cfg.AddProfile<MessageViewModelProfil>();
                    cfg.AddProfile<ContactViewModelProfil>();
                    cfg.AddProfile<ParcoursViewModelProfile>();
                });



                Mapper.AssertConfigurationIsValid();
            }
        }
}