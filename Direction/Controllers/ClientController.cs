using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Direction.Models.AccountViewModels;
using AutoMapper;
using SERVICES.UtilisateurSvc;
using SERVICES.ImageSvc;
using SERVICES.RoleSvc;
using POCO;

namespace Direction.Controllers
{
    public class ClientController : Controller
    {
        public IClientService ClientService { get; set; }
        public IRoleService RoleService { get; set; }
        public IImageService ImageService { get; set; }
        public IUtilisateurService<Client> UtilisateurService { get; set; }

        public ClientController(IClientService clientService, IRoleService roleService, IUtilisateurService<Client> utilisateurService, IImageService imageService)
        {
            ClientService = clientService;
            RoleService = roleService;
            UtilisateurService = utilisateurService;
            ImageService = imageService;
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult Consulter()
        {
            //On récupère tout les Client
            var clients = UtilisateurService.GetEntitiesByRole(Privilege.Client);

            if (clients.IsValid)
            {
                //On les ajoute au ViewModel
                IList<ClientViewModel> ListeClientViewModel = new List<ClientViewModel>();
                ListeClientViewModel = Mapper.Map<IList<Client>, IList<ClientViewModel>>(clients.Valeur);

                return View(ListeClientViewModel);
            }
            else
            {
                //Pour les dev
                foreach (var erreur in clients.Erreurs)
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
        public ActionResult Inscrire(HttpPostedFileBase file, ClientViewModel registerModel)
        {
            if (!ModelState.IsValid || (registerModel.Password != registerModel.ConfirmPassword))
            {
                return View(registerModel);

            }
            var createClientCommand = Mapper.Map<ClientViewModel, CreateClientCommand>(registerModel);

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

                //string path = Url.Content("~/Content/themes/base/images/Content/Account/DefaultAvatar.jpg");
                //System.Drawing.Image defaultImage = System.Drawing.Image.FromStream();

                string path = Server.MapPath("////Content/themes/base/images/Content/Account/DefaultAvatar.jpg");
                System.Drawing.Image defaultImage = System.Drawing.Image.FromFile(path);

                doc = ImageService.Resize(null, 80, 100, defaultImage);
            }

            //On ajoute l'avatar a la commande
            createClientCommand.Avatar = doc;

            //On ajoute le Privilege a la commande, ici, toujours un CLIENT !
            createClientCommand.Role = Privilege.Client;

            var result = ClientService.CreateClient(createClientCommand);

            if (result.IsValid)
            {
                var resultat = UtilisateurService.ValidateEntity(result.Valeur.Pseudo, registerModel.Password);

                if (resultat.IsValid)
                {
                    TempData["Message"] = "Le client a ete cree avec succes !";
                    return RedirectToAction("Inscrire", "Client");
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
        public ActionResult CreatePopupClient(int id)
        {
            ClientViewModel clientViewModel = new ClientViewModel();

            var client = UtilisateurService.GetEntityById(id);
            clientViewModel = Mapper.Map<Client, ClientViewModel>(client.Valeur);

            return PartialView(clientViewModel);
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult Migration()
        {
            //On récupère tout les Client
            var clients = UtilisateurService.GetEntitiesByRole(Privilege.Forum);

            if (clients.IsValid)
            {
                //On les ajoute au ViewModel
                IList<MigrationViewModel> ListeForumViewModel = new List<MigrationViewModel>();
                ListeForumViewModel = Mapper.Map<IList<Client>, IList<MigrationViewModel>>(clients.Valeur);

                return View(ListeForumViewModel);
            }
            else
            {
                //Pour les dev
                foreach (var erreur in clients.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult MigrationPopupClient(int id)
        {
            var client = UtilisateurService.GetEntityById(id);
            var migration = ClientService.MigrateToCompteClient(client.Valeur);

            TempData["Message"] = "L utilisateur a ete migre avec succes vers un compte client !";
            return RedirectToAction("Migration");


            //Thibault : C'étais tellement mieux.. !

            //MigrationViewModel migrationViewModel = new MigrationViewModel();
            //var client = UtilisateurService.GetEntityById(id);
            //migrationViewModel = Mapper.Map<Client, MigrationViewModel>(client.Valeur);
            //return PartialView(migrationViewModel);


            //Damien: Pas Forcement !!!!!!!!!!!!
            //En passant par l'automapping, tu crée une Command de migration et tu l'envoie a la methode migrationCompte, generique pour tout type de migration, TOC
        }

    }
}
