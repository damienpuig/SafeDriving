using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.VehiculeSvc
{
    public class UpdateVehiculeCommand
    {
        public string Nom { get; set; }
        public int SessionId { get; set; }
        public IList<Session> Sessions { get; set; }
    }
}
