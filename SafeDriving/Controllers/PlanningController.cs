using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeDriving.Models.PlanningViewModels;
using SERVICES.UtilisateurSvc;
using POCO;
using SERVICES.PlanningSvc;
using AutoMapper;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace SafeDriving.Controllers
{

    // Ce Controller prouve qu'un ViewModel doit etre constitué de propriété de type string s'il veut etre reconstruit et catcher en Ajax.
    public class PlanningController : Controller
    {

        // Service Utilisé
        public IUtilisateurService<Client> ClientService { get; set; }
        public IPlanningService PlanningService { get; set; }


        public PlanningController(IUtilisateurService<Client> clientService, IPlanningService planningService)
        {
            ClientService = clientService;
            PlanningService = planningService;
        }



        //
        // GET: /Planning/
        public ActionResult Index()
        {
            return View();
        }


        //Recuperation de la liste des offres
        [HttpGet]
        public JsonResult FetchOffres()
        {

            var pseudo = ClientService.GetUtilisateurCourantPseudo();

            if (pseudo.IsValid)
            {
                var compteClient = ClientService.GetEntityByPseudo(pseudo.Valeur);

                if (compteClient.IsValid)
                {


                    var listeOffre = PlanningService.GetIListOffreByClient(compteClient.Valeur);

                    if (listeOffre.IsValid)
                    {

                        var listeDto = listeOffre.Valeur.Select(x => new PlanningItemDto { text = x.Nom, value = x.Id.ToString() }).ToList();

                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        var JSON5 = jss.Serialize(listeDto);

                        return Json(JSON5, JsonRequestBehavior.AllowGet);
                    }

                }


            }
            return Json(new { Success = false });
        }


        //Recuperation de la liste des type de session par offre ( souvent 1 )
        [HttpPost]
        public JsonResult FetchTypeSession()
        {
            if (Request.IsAjaxRequest())
            {
                int SelectedOffre;

                if (Int32.TryParse(Request.Form["SelectedListeOffre"], out SelectedOffre))
                {
                    var pseudo = ClientService.GetUtilisateurCourantPseudo();

                    if (pseudo.IsValid)
                    {
                        var compteClient = ClientService.GetEntityByPseudo(pseudo.Valeur);

                        if (compteClient.IsValid)
                        {

                            var listeTypeSession = PlanningService.GetListTypeSessionByOffre(compteClient.Valeur, SelectedOffre);

                            if (listeTypeSession.IsValid)
                            {

                                var listeDto = listeTypeSession.Valeur.Select(x => new PlanningItemDto { text = x.Nom, value = x.Id.ToString() });


                                JavaScriptSerializer jss = new JavaScriptSerializer();
                                var JSON5 = jss.Serialize(listeDto);

                                return Json(JSON5, JsonRequestBehavior.AllowGet);
                            }

                        }


                    }
                   
                }
            } return Json(new { Success = false });
        }

        [HttpPost]
        public JsonResult FetchSession()
        {
            if (Request.IsAjaxRequest())
            {
                Regex newRegex = new Regex("%2f");
                Regex newRegex2 = new Regex("0");
                string currentDate = newRegex.Replace(Request.Form["SelectedDate"].ToString(), "/");

                //ehehehheheheheheheheheheehheehehhe
                if (newRegex2.Matches(currentDate).Count > 1)
                {
                    currentDate = newRegex2.Replace(currentDate, "");
                }
                


                //C'est effroyable...
                int? SelectedTypeSessionId = Convert.ToInt32(Request.Form["SelectedListeTypeSession"]);


                DateTime SelectedDate = new DateTime(2011, 06, 16);
                    //DateTime.Parse(currentDate);

                if (SelectedTypeSessionId != null && SelectedDate != null )
                {
                    
                    var listeSessionLibre = PlanningService.GetListFreesessionByTypeSessionAndDate(Convert.ToInt32(SelectedTypeSessionId), SelectedDate);

                    if (listeSessionLibre.IsValid)
                    {

                        var listeDto = listeSessionLibre.Valeur.Select(x => new PlanningItemDto { text = x.Nom, value = x.Id.ToString() });

                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        var JSON5 = jss.Serialize(listeDto);

                        return Json(JSON5, JsonRequestBehavior.AllowGet);
                    }

                }


            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public JsonResult PostQueryOverSession()
        {
              if (Request.IsAjaxRequest())
            {
                int SelectedOffre;              
                int SelectedSessionId = Convert.ToInt32(Request.Form["SelectedListeSession"]);

                if (Int32.TryParse(Request.Form["SelectedListeOffre"], out SelectedOffre))
                {
                    var pseudo = ClientService.GetUtilisateurCourantPseudo();

                    if (pseudo.IsValid)
                    {
                        var compteClient = ClientService.GetEntityByPseudo(pseudo.Valeur);

                        if (compteClient.IsValid)
                        {

                            var listeTypeSession = PlanningService.CreatePlanningInscription(new CreatePlanningCommand() { ClientId = compteClient.Valeur.Id, SelectedOffreId = SelectedOffre, SelectedSessionId= SelectedSessionId});

                           return Json(new { Success = true });
                        }


                    }
                   
                }
            } return Json(new { Success = false });
        }
        }

    }
