using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Formateur.Models.TopicViewModels;

namespace Formateur.Models.CategorieViewModels
{
    public class CategorieViewModel
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public IList<TopicViewModel> ListeTopic { get; set; }

        public string NomCreateur { get; set; }

        public string TitreNewTopic { get; set; }

        public string MessageNewTopic { get; set; }

        public string TitreNewCategorie { get; set; }

    }
}