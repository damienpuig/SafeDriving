using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.ExamenSvc
{
    public class ClientDto
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public DateTime DateNaissance { get; set; }

        public string Mail { get; set; }

        public string Telephone { get; set; }

        public string Adresse { get; set; }

        public string CodePostal { get; set; }

        public string Ville { get; set; }

        public float NoteMoyenne { get; set; }

        public IList<float> ListeNote { get; set; }
    }
}
