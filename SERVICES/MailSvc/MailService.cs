using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace SERVICES.MailSvc
{
    public class MailService : IMailService
    {

        public string UtilisateurSmtp { get; set; }
        public string PasswordSmtp { get; set; }
        public string ServeurSmtp { get; set; }
        public int PortSmtp { get; set; }
        public string AdresseFrom { get; set; }

        public MailService()
        {
            UtilisateurSmtp = ConfigurationManager.AppSettings["SmtpUtilisateur"];
            PasswordSmtp = ConfigurationManager.AppSettings["SmtpPassword"];
            ServeurSmtp = ConfigurationManager.AppSettings["SmtpServeur"];
            PortSmtp = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            AdresseFrom = ConfigurationManager.AppSettings["AdresseFrom"];
        }

        public Resultat<bool> SendMail(SendMailCommand sendMailCommand)
        {
            return Resultat<bool>.SafeExecute<MailService>(
                result =>
                {
                    MailMessage message = new MailMessage(sendMailCommand.Mail, AdresseFrom);
                    MailAddress adresse = new MailAddress(sendMailCommand.Mail);
                    message.Subject = sendMailCommand.Sujet;
                    message.Body = sendMailCommand.Prenom + " " + sendMailCommand.Nom + "(" + "Mail :" + sendMailCommand.Mail + ")" + " : " + sendMailCommand.Message;
                    message.From = adresse;
                    SmtpClient smtpClient = new SmtpClient(ServeurSmtp, PortSmtp);
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(UtilisateurSmtp, PasswordSmtp);

                    smtpClient.Send(message);

                });
        }
    }
}
