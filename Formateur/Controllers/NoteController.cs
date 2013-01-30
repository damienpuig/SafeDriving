using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES.UtilisateurSvc;
using SERVICES.RoleSvc;
using SERVICES.ImageSvc;
using POCO;
using Formateur.Models.AccountViewModels;
using AutoMapper;

namespace Formateur.Controllers
{
    public class NoteController : Controller
    {
        public IEmployeService EmployeService { get; set; }
        public IRoleService RoleService { get; set; }
        public IImageService ImageService { get; set; }
        public IUtilisateurService<Employe> UtilisateurService { get; set; }

        public NoteController(IEmployeService employeService, IRoleService roleService, IUtilisateurService<Employe> utilisateurService, IImageService imageService)
        {
            EmployeService = employeService;
            RoleService = roleService;
            UtilisateurService = utilisateurService;
            ImageService = imageService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ConsulterNote");
        }

        public ActionResult ConsulterNote()
        {
            var pseudo = UtilisateurService.GetUtilisateurCourantPseudo();

            if (pseudo.IsValid)
            {
                var FormateurCourant = UtilisateurService.GetEntityByPseudo(pseudo.Valeur);

                if (FormateurCourant.IsValid)
                {

                
                    //On les ajoute au ViewModel

                    var listClientSessionNoteDto = EmployeService.GetClientsNoteByFormateur(FormateurCourant.Valeur);

                    IList<ClientCustomViewModel> ListeClientViewModel = new List<ClientCustomViewModel>();

                    ListeClientViewModel = Mapper.Map<IList<ClientNoteDto>, IList<ClientCustomViewModel>>(listClientSessionNoteDto.Valeur);

                    return View(ListeClientViewModel);
                }
                else
                {
                    //Pour les gens (affiche la page error.cshtml)
                    throw new Exception("Erreur inattendue dans l'application");
                }

            }

            return RedirectToAction("Index", "Home");
           
        }


        public ActionResult NotePopupClient(int id)
        {
                 var pseudo = UtilisateurService.GetUtilisateurCourantPseudo();

                 if (pseudo.IsValid)
                 {
                     var FormateurCourant = UtilisateurService.GetEntityByPseudo(pseudo.Valeur);

                     if (FormateurCourant.IsValid)
                     {


                         //On les ajoute au ViewModel
                         var currentClientSessionNoteDto = EmployeService.GetHistoriqueSession(FormateurCourant.Valeur, id);

                         

                         var clientViewModel = Mapper.Map<ClientNoteDto, ClientCustomViewModel>(currentClientSessionNoteDto.Valeur);

                         return PartialView(clientViewModel);
                     }
                     else
                     {
                         //Pour les gens (affiche la page error.cshtml)
                         throw new Exception("Erreur inattendue dans l'application");
                     }
                 }

            return View();
        }


       [HttpPost]
        public ActionResult GoNote(ClientCustomViewModel model)
        {

            if (ModelState.IsValid)
            {

                var command = Mapper.Map<ClientCustomViewModel, CreateClientNoteCommand>(model);
                var resultatnotation = EmployeService.AddNoteToClientHistoricalSession(command);

                if (resultatnotation.Valeur == true)
                {
                    return RedirectToAction("ConsulterNote", "Note");
                }
            }

            return RedirectToAction("Index", "Home");
        }
        
    }
}
