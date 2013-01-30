using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Direction.Models.AccountViewModels
{
    public class MigrationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Pseudo")]
        public string Pseudo { get; set; }

        [Display(Name = "Adresse Postale")]
        public string AdressePostale { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }

        [Display(Name = "Ville")]
        public string Ville { get; set; }

        [Display(Name = "Téléphone")]
        public string Tel { get; set; }

        [Display(Name = "Date de naissance")]
        public string DateNaissance { get; set; }

        [Display(Name = "A déja le code")]
        public bool IsCoded { get; set; }
    }
}