using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Drawing;
using System.Text.RegularExpressions;

namespace SafeDriving.Models
{

    #region Models

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [SafeDriving.Helpers.AttributesHelpers.ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }


    public class RegisterModel
    {
        [Required(ErrorMessage = "* Prénom requis")]
        [Display(Name = "Prénom *")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "* Nom requis")]
        [Display(Name = "Nom *")]
        public string Nom { get; set; }

        [SafeDriving.Helpers.AttributesHelpers.ValidatePseudoIsUnique]
        [Required(ErrorMessage = "* Pseudo requis")]
        [Display(Name = "Pseudo *")]
        public string Pseudo { get; set; }

        [Required(ErrorMessage = "* Adresse requise")]
        [Display(Name = "Adresse Postale *")]
        public string AdressePostale { get; set; }

        [Required(ErrorMessage = "* Email requis")]
        [DataType(DataType.EmailAddress)]
        [SafeDriving.Helpers.AttributesHelpers.ValidateMail]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Code postal requis")]
        [Display(Name = "Code Postal *")]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "* Ville requise")]
        [Display(Name = "Ville *")]
        public string Ville { get; set; }

        [Required (ErrorMessage="* Mot de passe requis")]
        [SafeDriving.Helpers.AttributesHelpers.ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe *")]
        public string Password { get; set; }

        [Required(ErrorMessage = "* Confirmation requise")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation *")]
        [Compare("Password", ErrorMessage = "Confirmez votre mot de passe")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "* Téléphone requis")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Tel *")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "* Date de naissance requise")]
        [Display(Name = "Date de naissance *")]
        public string DateNaissance { get; set; }

        [Required(ErrorMessage = "* Indiquez si vous avez déja le code")]
        [Display(Name = "J'ai déja le code *")]
        public bool IsCoded { get; set; }

        [Required(ErrorMessage = "* Indiquez que vous acceptez les conditions")]
        [Display(Name = "J'ai lu et j'accepte les conditions *")]
        public bool Condition { get; set; }
    }

    public class CompteModel
    {
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Pseudo")]
        public string Pseudo { get; set; }

        [Display(Name = "Adresse Postale")]
        public string AdressePostale { get; set; }

        [SafeDriving.Helpers.AttributesHelpers.ValidateMail]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }

        [Display(Name = "Ville")]
        public string Ville { get; set; }

        [Display(Name = "Tel")]
        public string Tel { get; set; }

        [Display(Name = "Date de naissance ")]
        public string DateNaissance { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        [Compare("Password", ErrorMessage = "Confirmez votre mot de passe")]
        public string ConfirmPassword { get; set; }
    }

    #endregion
}
