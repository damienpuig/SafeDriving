using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using POCO;

namespace Direction.Models.ArticleViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Titre requis")]
        public string Titre { get; set; }

        public string Date { get; set; }

        [Required(ErrorMessage = "* Contenu requis")]
        public string Contenu { get; set; }

        public int EmployeId { get; set; }

        public Employe Employe { get; set; }

        [Required(ErrorMessage = "* Type d'article requis")]
        public int TypeArticleId { get; set; }

        public IDictionary<int, string> ListeCategorie { get; set; }
    }
}