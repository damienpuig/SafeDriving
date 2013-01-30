using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.SessionSvc
{
    public class SessionDto
    {
        public int id { get; set; }

        public string title { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

    }
}
