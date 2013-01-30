using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.TypeSessionSvc
{
    public class UpdateTypeSessionCommand
    {
        public string Nom { get; set; }
        public IList<Session> Sessions { get; set; }
    }
}
