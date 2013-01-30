using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using System.Drawing;
using SERVICES.RoleSvc;

namespace SERVICES.UtilisateurSvc
{
   public class CreateClientCommand
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Pseudo { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; } 
        public DateTime DateNaissance { get; set; }
        public Privilege Role { get; set; }
        public bool IsCoded { get; set; }
    }
}
