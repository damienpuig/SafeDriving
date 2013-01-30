using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.MailSvc
{
    public class SendMailCommand
    {
        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public string Sujet { get; set; }

        public string Message { get; set; }
    }
}
