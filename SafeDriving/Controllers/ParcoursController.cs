using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES.ParcoursSvc;
using SERVICES.UtilisateurSvc;
using POCO;
using AutoMapper;
using System.Runtime.Serialization;
using SafeDriving.Models.ParcoursViewModels;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace SafeDriving.Controllers
{
    public class ParcoursController : Controller
    {

        public IParcoursService ParcoursService { get; set; }
        public IUtilisateurService<Client> ClientService { get; set; }

        public ParcoursController(IParcoursService parcoursService, IUtilisateurService<Client> clientService)
        {
            ParcoursService = parcoursService;
            ClientService = clientService;
        }

        //
        // GET: /Parcours/
        public ActionResult Index()
        {
          return View();
        }

        [HttpGet]
        public JsonResult fetchData()
        {


        var pseudo = ClientService.GetUtilisateurCourantPseudo();
            if (pseudo.IsValid)
            {

                var client = ClientService.GetEntityByPseudo(pseudo.Valeur);

                if (client.IsValid)
                {
                    var parcoursDto = ParcoursService.GetParcoursSessionByClient(client.Valeur);

                    if (parcoursDto.IsValid)
                    {

                        var parcoursViewModel = Mapper.Map<ParcoursDto, ParcoursViewModel>(parcoursDto.Valeur);
                        
                        //Rox du poney
                        var JSON2 = Serialize<Dictionary<OffreReferenceDto, IList<SessionHistoriqueDto>>>(parcoursViewModel.GraphDictionnary);


                        //Le retour Ici ouah
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        IList<Session> ListeSessionReturn = jss.Deserialize<IList<Session>>(JSON2);


                        return Json(JSON2, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(new { Success = false });
        }

        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

    }
  
}
