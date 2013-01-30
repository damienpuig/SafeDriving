using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ArticleSvc
{
   public class CreateArticleCommand
    {
        public string Titre { get; set; }

        public DateTime Date { get; set; }

        public string Contenu { get; set; }

        public int EmployeId { get; set; }

        public int TypeArticleId  { get; set; }
    }
}
