using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Direction.Models.CategorieViewModels;
using Direction.Models.TopicViewModels;
using Direction.Models.MessageViewModels;
using Direction.Models.AccountViewModels;
using SERVICES.UtilisateurSvc;
using SERVICES.SessionSvc;

namespace Direction.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<CategorieViewModelProfil>();
                cfg.AddProfile<TopicViewModelProfil>();
                cfg.AddProfile<MessageViewModelProfil>();
                cfg.AddProfile<AccountViewModelProfil>();
                cfg.AddProfile<ClientProfile>();
                cfg.AddProfile<EmployeProfile>();
                cfg.AddProfile<SessionProfile>();
            });



            Mapper.AssertConfigurationIsValid();
        }
    }
}