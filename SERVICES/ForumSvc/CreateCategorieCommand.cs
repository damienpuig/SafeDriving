using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.ForumSvc
{
    public class CreateCategorieCommand
    {
        public string Nom { get; set; }

        public DateTime DateCreation { get; set; }

        public int CreateurId { get; set; }

    }
}
