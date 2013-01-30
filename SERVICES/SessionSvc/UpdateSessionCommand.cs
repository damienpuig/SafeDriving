using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.SessionSvc
{
    public class UpdateSessionCommand
    {
        public int TypeSessionId { get; set; }
        public int EmployeId { get; set; }
        public string Nom { get; set; }
        public int CircuitId { get; set; }
        public int Date_Id { get; set; }
        public Circuit Circuit { get; set; }
        public Employe Employe { get; set; }
        public TypeSession TypeSession { get; set; }
        public IList<Vehicule> Vehicules { get; set; }
    }
}
