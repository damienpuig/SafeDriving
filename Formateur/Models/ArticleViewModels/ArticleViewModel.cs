using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POCO;

namespace Formateur.Models.ArticleViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Titre { get; set; }

        public string Date { get; set; }

        public string Contenu { get; set; }

        public int EmployeId { get; set; }

        public Employe Employe { get; set; }

    }
}