using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Direction.Models.ExamenViewModels
{
    public class ExamenViewModel
    {   
        [Display(Name="Selectionnez un type d'examen :")]
        public Dictionary<int,string> TypeExamen { get; set; }

        [Required(ErrorMessage="Selectionnez un type d'examen")]
        public int SelectedTypeExamen { get; set; }

        [Display(Name = "Nombre de place :")]
        [Required(ErrorMessage="Saisir un nombre de place pour l'examen")]
        [Range(0,100, ErrorMessage="Impossible de sélectionner plus de 100 élèves !")]
        [RegularExpression("[0-9]{1,}")]
        public string NbPlace { get; set; }

        public IList<BestClientViewModel> ListeBestClient { get; set; }
        
    }
}