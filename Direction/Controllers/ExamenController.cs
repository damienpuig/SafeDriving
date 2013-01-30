using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES.RoleSvc;
using Direction.Models.ExamenViewModels;
using SERVICES.ExamenSvc;
using Direction.Models.AccountViewModels;

namespace Direction.Controllers
{
    public class ExamenController : Controller
    {
        public IExamenService ExamenService { get; set; }

        public ExamenController(IExamenService examenService)
        {
            ExamenService = examenService;
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult Index()
        {
            var examenViewModel = new ExamenViewModel();

            // Les clés du dico ne correspondent pas au ID des type d'examen en base 
            // mais aux Id des types d'examen noté pour faciliter la recherche des bestClient
            var typeExamen = new Dictionary<int, string>();

            typeExamen.Add(5, "Code de la route");
            typeExamen.Add(2, "Permis Moto");
            typeExamen.Add(3, "Permis Voiture");

            examenViewModel.TypeExamen = typeExamen;

            return View(examenViewModel);
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        [HttpPost]
        public ActionResult Index(ExamenViewModel examenViewModel)
        {

            return View();
        }

        public ActionResult GetBestClient(ExamenViewModel examenViewModel)
        {
            //On récupère toutes les sessions du type selectionné
            var listeSession = ExamenService.GetSessionByTypeSessionId(examenViewModel.SelectedTypeExamen);

            //On récupère toutes les client-sessions (historique) du type selectionné
            var listeHistorique = ExamenService.GetUtilisateurs_ClientSessionsBySessionId(listeSession);
            
            //On récupère une liste de Client Dto avec leurs note moyenne
            var listeClientDto = ExamenService.GetClientByHistorique(listeHistorique);
            listeClientDto = ExamenService.GetMoyenneForClientDto(listeClientDto);

            var orderClientDto = listeClientDto.OrderByDescending(x => x.NoteMoyenne);

            int nbPlace = Convert.ToInt32(examenViewModel.NbPlace);
            var finalListeClientDto = orderClientDto.Take(nbPlace).ToList();

            var listeClientViewModel = new List<BestClientViewModel>();
            foreach (var clientDto in finalListeClientDto)
            {
                var bestClientViewModel = new BestClientViewModel { AdressePostale=clientDto.Adresse, CodePostal=clientDto.CodePostal, Email=clientDto.Mail, Id=clientDto.Id, Nom=clientDto.Nom, Ville=clientDto.Ville, Tel=clientDto.Telephone, Prenom=clientDto.Prenom, DateNaissance=clientDto.DateNaissance.ToString(), Moyenne=clientDto.NoteMoyenne.ToString(), NbNote=clientDto.ListeNote.Count().ToString() };
                listeClientViewModel.Add(bestClientViewModel);
            }
            examenViewModel.ListeBestClient = listeClientViewModel;

            if (examenViewModel.ListeBestClient.Count == 0)
                TempData["Message"] = "Aucun client trouve !";

            return PartialView("GetBestClientPartial", examenViewModel);
        }


    }
}
