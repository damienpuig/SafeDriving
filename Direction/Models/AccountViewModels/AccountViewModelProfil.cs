using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SERVICES.UtilisateurSvc;
using Direction.AutoMapper;
using POCO;

namespace Direction.Models.AccountViewModels
{
    public class AccountViewModelProfil : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "AccountViewModelProfil";
            }
        }

        protected override void Configure()
        {
            CreateMap<ClientViewModel, CreateClientCommand>()
                .ForMember(x => x.Adresse, o => o.MapFrom(n => n.AdressePostale))
                .ForMember(x => x.DateNaissance, o => o.ResolveUsing<DateTimeCustomResolver.StringToDateTimeResolver>().FromMember(n => n.DateNaissance))
                .ForMember(x => x.Telephone, o => o.MapFrom(n => n.Tel))
                .ForMember(x => x.Mail, o => o.MapFrom(n => n.Email))
                .ForMember(x => x.Avatar, o => o.Ignore())
                .ForMember(x => x.Role, o => o.Ignore())
                ;

            CreateMap<EmployeViewModel, CreateClientCommand>()
                .ForMember(x => x.Adresse, o => o.MapFrom(n => n.AdressePostale))
                .ForMember(x => x.DateNaissance, o => o.Ignore())
                .ForMember(x => x.Telephone, o => o.MapFrom(n => n.Tel))
                .ForMember(x => x.Mail, o => o.MapFrom(n => n.Email))
                .ForMember(x => x.Avatar, o => o.Ignore())
                .ForMember(x => x.Role, o => o.Ignore())
                .ForMember(x => x.IsCoded, o => o.Ignore())
                ;

            CreateMap<Client, ClientViewModel>()
                .ForMember(x=>x.ConfirmPassword, o=>o.Ignore())
                .ForMember(x => x.DateNaissance, o => o.ResolveUsing<DateTimeCustomResolver.DateTimeToStringResolver>().FromMember(n => n.DateNaissance))
                .ForMember(x => x.AdressePostale, o => o.MapFrom(n => n.Adresse))
                .ForMember(x => x.Email, o => o.MapFrom(n => n.Mail))
                .ForMember(x => x.Tel, o => o.MapFrom(n => n.Telephone))
                ;

            CreateMap<Client, MigrationViewModel>()
                .ForMember(x => x.DateNaissance, o => o.ResolveUsing<DateTimeCustomResolver.DateTimeToStringResolver>().FromMember(n => n.DateNaissance))
                .ForMember(x => x.AdressePostale, o => o.MapFrom(n => n.Adresse))
                .ForMember(x => x.Email, o => o.MapFrom(n => n.Mail))
                .ForMember(x => x.Tel, o => o.MapFrom(n => n.Telephone))
                ;

            CreateMap<Employe, EmployeViewModel>()
                .ForMember(x => x.ConfirmPassword, o => o.Ignore())
                .ForMember(x => x.AdressePostale, o => o.MapFrom(n => n.Adresse))
                .ForMember(x => x.Email, o => o.MapFrom(n => n.Mail))
                .ForMember(x => x.Tel, o => o.MapFrom(n => n.Telephone))
                ;
        }
    }
}