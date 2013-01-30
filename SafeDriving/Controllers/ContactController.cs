using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeDriving.Models.ContactViewModel;
using SERVICES.MailSvc;
using AutoMapper;

namespace SafeDriving.Controllers
{
    public class ContactController : Controller
    {
        public IMailService MailService { get; set; }

        public ContactController(IMailService mailService)
        {
            MailService = mailService;
        }

        public ActionResult Index()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactViewModel);
            }
            else
            {
                var sendMailCommand = Mapper.Map<ContactViewModel, SendMailCommand>(contactViewModel);
                var result = MailService.SendMail(sendMailCommand);

                if (result.IsValid)
                {
                    ViewData["Message"] = "Message envoye avec succes !";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    //Pour les dev
                    foreach (var erreur in result.Erreurs)
                    {
                        ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                    }

                    //Pour les gens (affiche la page error.cshtml
                    throw new Exception("Erreur inattendue dans l'application");
                }
            }
        }

    }
}
