using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.SessionSvc
{
   public class CreateSessionCommand
    {
        public int TypeSessionId { get; set; }
        public int EmployeId { get; set; }
        public string Nom { get; set; }
        public int CircuitId{ get; set; }
        public int  Date_Id { get; set; }
        public int NbParticipant { get; set; }
        public int EtatSessionsId { get; set; }
        public Nullable<System.DateTime> HeureDebut { get; set; }
        public Nullable<System.DateTime> HeureFin { get; set; }
    }
}
