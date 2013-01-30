using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SafeDriving.Models.ContactViewModel
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Nom requis")]
        [Display(Name = "Nom *")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "* Prénom requis")]
        [Display(Name = "Prénom *")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "* Email requis")]
        [DataType(DataType.EmailAddress)]
        [SafeDriving.Helpers.AttributesHelpers.ValidateMail]
        [Display(Name = "Email *")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "* Sujet requis")]
        [Display(Name = "Sujet *")]
        public string Sujet { get; set; }

        [Required(ErrorMessage = "* Message requis")]
        [Display(Name = "Message *")]
        public string Message { get; set; }

    }
}