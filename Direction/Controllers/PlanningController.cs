using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES.RoleSvc;
using SERVICES.UtilisateurSvc;
using POCO;
using SERVICES.PlanningSvc;
using Direction.Models.PlanningViewModels;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using SERVICES.SessionSvc;
using System.Web.Script.Serialization;

namespace Direction.Controllers
{
    public class PlanningController : Controller
    {
        public IUtilisateurService<Employe> UtilisateurService { get; set; }
        public IPlanningService PlanningService { get; set; }
        public ISessionService SessionService { get; set; }

        public PlanningController(IUtilisateurService<Employe> utilisateurService, IPlanningService planningService, ISessionService sessionService)
        {
            UtilisateurService = utilisateurService;
            PlanningService = planningService;
            SessionService = sessionService;
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role= Privilege.Admin )]
        public ActionResult Index()
        {
            //On récupère tous les formateurs, dans un dico
            var listeFormateur = PlanningService.GetAllFormateur(Privilege.Formateur);

            if (listeFormateur.IsValid)
            {
                var planningViewModel = new PlanningViewModel();
                //On charge le dico dans le VM
                planningViewModel.ListeFormateur = listeFormateur.Valeur;
                return View(planningViewModel);
            }
            else
            {
                //Pour les dev
                foreach (var erreur in listeFormateur.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        [HttpPost]
        public ActionResult GetSession(int idFormateur)
        {
            //On récupère la liste des sessions en fonction des formateurs
            var sessions = SessionService.GetSessionByFormateur(idFormateur);

            if (sessions.IsValid)
            {
                var listeSessionDto = new List<SessionDto>();
                foreach (var session in sessions.Valeur)
                {
                    var sessionDto = new SessionDto { id=session.Id, title=session.Nom, start=session.HeureDebut.Value, end=session.HeureFin.Value  };
                    listeSessionDto.Add(sessionDto);
                }

                var listeSessionJson = Serialize<List<SessionDto>>(listeSessionDto);

                return Json(listeSessionJson, JsonRequestBehavior.AllowGet);

            }
            else
            {
                //Pour les dev
                foreach (var erreur in sessions.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult GetSessionById(int idSession)
        {
            var sessionsResult = SessionService.GetSessionById(idSession);
            if (sessionsResult.IsValid)
            {
                var sessionViewModel = new SessionViewModel { Id=sessionsResult.Valeur.Id, Circuit=sessionsResult.Valeur.Circuit.Nom, TypeSession=sessionsResult.Valeur.TypeSession.Nom, NbParticipant=sessionsResult.Valeur.NbParticipant.ToString() };
                string session = Serialize<SessionViewModel>(sessionViewModel);

                return Json(session, JsonRequestBehavior.AllowGet);
                
            }
            else
            {
                //Pour les dev
                foreach (var erreur in sessionsResult.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        [HttpPost]
        public ActionResult GetCircuitAndTypeSessionByStartDate(string startDate)
        {
            var dateDebut = ConvertDateFromString(startDate);
            var result = SessionService.GetCircuitAndTypeSessionByDate(dateDebut);
            if (result.IsValid)
            {
                string circuitAndTypeSession = Serialize<Dictionary<string, List<string>>>(result.Valeur);

                return Json(circuitAndTypeSession, JsonRequestBehavior.AllowGet);
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

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        [HttpPost]
        public ActionResult CreateSession(string heureDebut, string heureFin, string sessionName, string typeSessionName, string circuitName, string nbParticipant, string formateurId)
        {
            if (sessionName == null || sessionName == "" || nbParticipant == null || nbParticipant == "")
            {
                return Json(new
                {
                    Success = false
                });
            }

            var dateDebut = ConvertDateFromString(heureDebut);
            var dateFin = ConvertDateFromString(heureFin);
            var nombreParticipant = Convert.ToInt32(nbParticipant);
            var idFormateur = Convert.ToInt32(formateurId);

            var employeResult = UtilisateurService.GetEntityById(idFormateur);

            var typeSessionResult = SessionService.GetTypeSessionByName(typeSessionName);

            var circuitResult = SessionService.GetCircuitByName(circuitName);

            if (!employeResult.IsValid || !typeSessionResult.IsValid || !circuitResult.IsValid)
            {
                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }

            //A l'ancienne, cousin, comme d'hab 
            var createSessionCommand = new CreateSessionCommand { CircuitId = circuitResult.Valeur.Id, Date_Id = 1, EmployeId = employeResult.Valeur.Id, Nom = sessionName, TypeSessionId = typeSessionResult.Valeur.Id, HeureDebut = dateDebut, HeureFin = dateFin, EtatSessionsId = 4, NbParticipant = nombreParticipant };
            
            //On crée la session
            var createSessionResult = SessionService.CreateSession(createSessionCommand);

            if (createSessionResult.IsValid)
            {
                return Json(new
                {
                    Success = true
                });
            }
            else
            {
                //Pour les gens (affiche la page error.cshtml
                return Json(new
                {
                    Success = false
                });
            }
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        [HttpPost]
        public ActionResult DeleteSession(string sessionName)
        {
            if (sessionName == null || sessionName == "")
            {
                return Json(new
                {
                    Success = false
                });
            }

            //On crée la session
            var deleteSessionResult = SessionService.RemoveSession(sessionName);

            if (deleteSessionResult.IsValid)
            {
                return Json(new
                {
                    Success = true
                });
            }
            else
            {
                //Pour les gens (affiche la page error.cshtml
                return Json(new
                {
                    Success = false
                });
            }

        }

        private DateTime ConvertDateFromString(string dateToConvert)
        {

            DateTime unixEpoch = new DateTime(1970, 1, 1, 2, 0, 0, 0, DateTimeKind.Utc);

            dateToConvert = dateToConvert.Substring(0, 10);

            DateTime myDateTime = unixEpoch.AddSeconds(Convert.ToInt32(dateToConvert));
            return myDateTime;


            //IFormatProvider us_format = System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat;

            ///* On a pas besoin des 4 derniers, SOUS CHROME */
            //for (int i = 0; i < 4; i++)
            //{
            //    dateToConvert = dateToConvert.Substring(0, dateToConvert.LastIndexOf(' '));
            //}
            ///* On a pas besoin du premier*/
            //dateToConvert = dateToConvert.Substring(dateToConvert.IndexOf(' ') + 1);

            //DateTime us_date = DateTime.ParseExact(dateToConvert, "MMM d yyyy H:m:s", us_format);

            //return us_date;
        }

        private static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

    }
}
