using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.MailSvc
{
    public interface IMailService
    {
        Resultat<bool> SendMail(SendMailCommand sendMailCommand);
    }
}
