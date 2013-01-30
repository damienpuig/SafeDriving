using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SERVICES.MailSvc;

namespace SafeDriving.Models.ContactViewModel
{
    public class ContactViewModelProfil : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ContactViewModelProfil";
            }
        }

        protected override void Configure()
        {
            CreateMap<ContactViewModel, SendMailCommand>();
        }
    }
}