using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Direction.Models.ExamenViewModels
{
    public class BestClientViewModel
    {
         public int Id { get; set; }

        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Adresse Postale")]
        public string AdressePostale { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }

        [Display(Name = "Ville")]
        public string Ville { get; set; }

        [Display(Name = "Tel")]
        public string Tel { get; set; }

        [Display(Name = "Date de naissance")]
        public string DateNaissance { get; set; }

        [Display(Name = "Moyenne")]
        public string Moyenne { get; set; }

        [Display(Name = "Nombre de note")]
        public string NbNote { get; set; }
    }
}