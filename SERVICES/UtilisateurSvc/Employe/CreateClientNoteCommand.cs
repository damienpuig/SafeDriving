using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.UtilisateurSvc
{
    public class CreateClientNoteCommand
    {
        public int IdHistoriqueSession { get; set; }

        public string Note { get; set; }
    }
}
