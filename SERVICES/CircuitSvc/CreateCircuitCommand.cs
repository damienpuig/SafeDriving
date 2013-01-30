using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.CircuitSvc
{
   public class CreateCircuitCommand
    {
       public string Nom { get; set; }

        public IList<Session> Sessions { get; set; } 
    }
}
