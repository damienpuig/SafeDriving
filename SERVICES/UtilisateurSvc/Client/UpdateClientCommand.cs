using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.UtilisateurSvc
{
    public class UpdateClientCommand
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public byte[] Avatar { get; set; }
        public string MotDePasse { get; set; }
        public string Pseudo { get; set; }
        public bool IsCoded { get; set; }
        public DateTime DateNaissance { get; set; }
        public IList<Offre> Offres { get; set; }
    }
}
