using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.DateSvc
{
   public class CreateDateCommand
    {

        public DateTime HeureDebut { get; set; }
        public DateTime HeureFin { get; set; }
        public DateTime Jour { get; set; }
        public int VehiculeId { get; set; }
        public int CircuitId { get; set; }
        public int EmployeId { get; set; }
        public int SessionId { get; set; }

        public Circuit Circuit { get; set; }
        public Employe Employe { get; set; }
        public IList<Session> Sessions { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
