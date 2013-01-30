using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Direction.Models.AccountViewModels
{
    public class EmployeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Prénom requis")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "* Nom requis")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Direction.Helpers.AttributeHelpers.ValidatePseudoIsUnique]
        [Required(ErrorMessage = "* Pseudo requis")]
        [Display(Name = "Pseudo")]
        public string Pseudo { get; set; }

        [Required(ErrorMessage = "* Adresse requise")]
        [Display(Name = "Adresse Postale")]
        public string AdressePostale { get; set; }

        [Required(ErrorMessage = "* Email requis")]
        [DataType(DataType.EmailAddress)]
        [Direction.Helpers.AttributeHelpers.ValidateMail]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Code postal requis")]
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "* Ville requise")]
        [Display(Name = "Ville")]
        public string Ville { get; set; }

        [Required(ErrorMessage = "* Mot de passe requis")]
        [Direction.Helpers.AttributeHelpers.ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Required(ErrorMessage = "* Confirmation requise")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        [Compare("Password", ErrorMessage = "Confirmez votre mot de passe")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "* Téléphone requis")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Tel")]
        public string Tel { get; set; }

    }
}