using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.ForumSvc
{
    public class CreateTopicCommand
    {
        public string Nom { get; set; }

        public DateTime DateCreation { get; set; }

        public int CreateurId { get; set; }

        public string Contenu { get; set; }

        public int CategorieId { get; set; }

    }
}
