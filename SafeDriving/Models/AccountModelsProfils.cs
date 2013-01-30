using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SERVICES.UtilisateurSvc;
using SafeDriving.AutoMapper;
using POCO;

namespace SafeDriving.Models
{
    public class AccountModelsProfils : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "AccountModelsProfils";
            }
        }

        protected override void Configure()
        {
            CreateMap<RegisterModel, CreateClientCommand>()
                .ForMember(x => x.Adresse, o => o.MapFrom(n => n.AdressePostale))
                .ForMember(x => x.DateNaissance, o => o.ResolveUsing<DateTimeCustomResolver.StringToDateTimeResolver>().FromMember(n => n.DateNaissance))
                .ForMember(x => x.Telephone, o => o.MapFrom(n => n.Tel))
                .ForMember(x => x.Mail, o => o.MapFrom(n => n.Email))
                .ForMember(x=>x.Avatar, o=>o.Ignore())
                .ForMember(x=>x.Role, o=>o.Ignore())
                ;

            CreateMap<Client, CompteModel>()
                .ForMember(x => x.AdressePostale, o => o.MapFrom(n => n.Adresse))
                .ForMember(x => x.DateNaissance, o => o.ResolveUsing<DateTimeCustomResolver.DateTimeToStringResolver>().FromMember(n => n.DateNaissance))
                .ForMember(x => x.Tel, o => o.MapFrom(n => n.Telephone))
                .ForMember(x => x.Email, o => o.MapFrom(n => n.Mail))
                .ForMember(x => x.ConfirmPassword, o => o.Ignore())
                ;

            CreateMap<CompteModel, UpdateClientCommand>()
                .ForMember(x => x.Adresse, o => o.MapFrom(n => n.AdressePostale))
                .ForMember(x => x.DateNaissance, o => o.ResolveUsing<DateTimeCustomResolver.StringToDateTimeResolver>().FromMember(n => n.DateNaissance))
                .ForMember(x => x.Telephone, o => o.MapFrom(n => n.Tel))
                .ForMember(x => x.Mail, o => o.MapFrom(n => n.Email))
                .ForMember(x => x.Avatar, o => o.Ignore())
                .ForMember(x => x.Offres, o => o.Ignore())
                .ForMember(x => x.IsCoded, o => o.Ignore())
                .ForMember(x => x.MotDePasse, o => o.ResolveUsing<UpdatePasswordCustomResolver.UpdatePasswordResolver>().FromMember(n=>n.Password))
                ;

        }
    }
}