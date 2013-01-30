using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.UtilisateurSvc
{
   public class ClientNoteDto
    {
        public int IdHistoriqueSession { get; set; }

        public string Prenom { get; set; }

        public string Nom { get; set; }

        public int IdSession { get; set; }

        public string NamedSession { get; set; }

        public string Date { get; set; }

        public string Note { get; set; }

    }
}
