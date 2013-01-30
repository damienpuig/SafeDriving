using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Direction.Models.AccountViewModels;
using SERVICES.UtilisateurSvc;
using SERVICES.RoleSvc;
using SERVICES.ImageSvc;
using POCO;
using AutoMapper;

namespace Direction.Controllers
{
    public class EmployeController : Controller
    {
        public IEmployeService EmployeService { get; set; }
        public IRoleService RoleService { get; set; }
        public IImageService ImageService { get; set; }
        public IUtilisateurService<Employe> UtilisateurService { get; set; }

        public EmployeController(IEmployeService employeService, IRoleService roleService, IUtilisateurService<Employe> utilisateurService, IImageService imageService)
        {
            EmployeService = employeService;
            RoleService = roleService;
            UtilisateurService = utilisateurService;
            ImageService = imageService;
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult Consulter()
        {
            //On récupère tout les Client
            var employes = UtilisateurService.GetEntitiesByRole(Privilege.Formateur);

            if (employes.IsValid)
            {
                //On les ajoute au ViewModel
                IList<EmployeViewModel> ListeEmployeViewModel = new List<EmployeViewModel>();
                ListeEmployeViewModel = Mapper.Map<IList<Employe>, IList<EmployeViewModel>>(employes.Valeur);

                return View(ListeEmployeViewModel);
            }
            else
            {
                //Pour les dev
                foreach (var erreur in employes.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult Inscrire()
        {
            return View();
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        [HttpPost]
        public ActionResult Inscrire(HttpPostedFileBase file, EmployeViewModel registerModel)
        {
            if (!ModelState.IsValid || (registerModel.Password != registerModel.ConfirmPassword))
            {
                return View(registerModel);
            }
            var createClientCommand = Mapper.Map<EmployeViewModel, CreateClientCommand>(registerModel);

            byte[] doc;

            // On utilise l'utilisateur a uploader uniquement des jpeg et des png
            if (file != null)
            {
                if ((file.ContentType == "image/jpeg" || file.ContentType == "image/x-png") && file.FileName.Length < 100 && file.ContentLength < 2000000)
                {

                    // on récupère le fichier dans un format exploitable (tableau de byte) et on le tronque a la volé !
                    doc = ImageService.Resize(file, 80, 100, null);
                    file.InputStream.Read(doc, 0, file.ContentLength);
                }
                else
                {
                    doc = new byte[1];
                }
            }
            else
            {
                //Default Avatar mise en place dans le service, on drop l'envoi par le null et on lui balance l'image par default

                string path = Server.MapPath("////Content/themes/base/images/Content/Account/DefaultAvatar.jpg");
                System.Drawing.Image defaultImage = System.Drawing.Image.FromFile(path);

                doc = ImageService.Resize(null, 80, 100, defaultImage);
            }

            //On ajoute l'avatar a la commande
            createClientCommand.Avatar = doc;

            //On ajoute le Privilege a la commande, ici, toujours un FORMATEUR !
            createClientCommand.Role = Privilege.Formateur;

            var result = EmployeService.CreateEmploye(createClientCommand);

            if (result.IsValid)
            {
                var resultat = UtilisateurService.ValidateEntity(result.Valeur.Pseudo, registerModel.Password);

                if (resultat.IsValid)
                {
                    TempData["Message"] = "L employe a ete cree avec succes !";
                    return RedirectToAction("Inscrire", "Employe");
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
        public ActionResult Pseudo(ClientViewModel model)
        {
            var result = UtilisateurService.IsPseudoUnique(model.Pseudo);

            if (result.IsValid)
            {
                if (result.Valeur == true)
                    return null;
                else
                {
                    return Json(new
                    {
                        Success = true
                    });
                }
            }
            else
            {
                return null;
            }
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult CreatePopupEmploye(int id)
        {
            EmployeViewModel employeViewModel = new EmployeViewModel();

            var employe = UtilisateurService.GetEntityById(id);
            employeViewModel = Mapper.Map<Employe, EmployeViewModel>(employe.Valeur);

            return PartialView(employeViewModel);
        }

    }
}
