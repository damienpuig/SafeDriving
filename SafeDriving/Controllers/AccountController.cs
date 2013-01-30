using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using SafeDriving.Models;
using AutoMapper;
using SERVICES.UtilisateurSvc;
using SERVICES.RoleSvc;
using SERVICES.ImageSvc;
using POCO;

namespace SafeDriving.Controllers
{
    public class AccountController : Controller
    {
        public IClientService ClientService { get; set; }
        public IRoleService RoleService { get; set; }
        public IImageService ImageService { get; set; }
        public IUtilisateurService<Client> UtilisateurService { get; set; }

        public AccountController(IClientService clientService, IRoleService roleService, IUtilisateurService<Client> utilisateurService, IImageService imageService)
        {
            ClientService = clientService;
            RoleService = roleService;
            UtilisateurService = utilisateurService;
            ImageService = imageService;
        }

        public ActionResult LogOn()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {

                if(!string.IsNullOrWhiteSpace(model.UserName)&&!string.IsNullOrWhiteSpace(model.Password))
                {
                     var result =  UtilisateurService.ValidateEntity(model.UserName, model.Password);

                     if (result.IsValid)
                     {
                            UtilisateurService.SignIn(result.Valeur, model.RememberMe);
                            return RedirectToAction("Index", "Account");
                     }
                     else
                     {
                         TempData["Message"] = "Vous n etes pas authorise a vous loguer ou n etes pas encore inscrit sur le site";
                         return RedirectToAction("Index", "Home");
                     }
                }  
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase file,RegisterModel registerModel)
        {
            if (!ModelState.IsValid || (registerModel.Password != registerModel.ConfirmPassword))
            {
                return View(registerModel);
                
            }
                var createClientCommand = Mapper.Map<RegisterModel, CreateClientCommand>(registerModel);

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

                //On ajoute le Privilege a la commande, ici, toujours un FORUM USER !
                createClientCommand.Role = Privilege.Forum;

                var result = ClientService.CreateClient(createClientCommand);

                if (result.IsValid)
                {
                    var resultat = UtilisateurService.ValidateEntity(result.Valeur.Pseudo, registerModel.Password);

                    if (resultat.IsValid)
                    {
                        UtilisateurService.SignIn(resultat.Valeur, false);
                        return RedirectToAction("Index", "Account");
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

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Index()
        {
            //On récupère le pseudo du l'utilisateur courant, il est unique donc OK
            var pseudo = UtilisateurService.GetUtilisateurCourantPseudo().Valeur;

            //On récupère le client correspondant
            var client = UtilisateurService.GetEntityByPseudo(pseudo);

            //Si il est valide
            if (client.IsValid)
            {
                //On le mappe
                var clientViewModel = Mapper.Map<Client, CompteModel>(client.Valeur);

                //On check le role
                var role = UtilisateurService.GetRoleByEntityId(client.Valeur.Id);
                if (role.Valeur == Privilege.Client)
                {
                    //GO
                    return View(clientViewModel);
                }
                else if (role.Valeur == Privilege.Forum)
                {
                    return View("IndexForumUser", clientViewModel);
                }
                else
                {
                    return View("Index", "Home");
                }
                
            }
            else
            {
                //Pour les dev
                foreach (var erreur in client.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

        [HttpPost]
        public ActionResult EditPartial(CompteModel model)
        {
            return Json(new
            {
                Success = true
            });
        }

        [HttpPost]
        public ActionResult Edit(CompteModel updateCompteModel)
        {
            if (!ModelState.IsValid || (updateCompteModel.Password != updateCompteModel.ConfirmPassword))
            {
                return View(updateCompteModel);
            }
            if (updateCompteModel.Password != null)
            {
                if (updateCompteModel.Password.Count() < 6)
                    return View(updateCompteModel);
            }
            else
            {
                //on mappe le viewmodel en commande
                var updateClientCommande = Mapper.Map<CompteModel, UpdateClientCommand>(updateCompteModel);

                var result = ClientService.UpdateEntity(updateClientCommande);

                if (result.IsValid)
                {

                    return RedirectToAction("Index");
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

            return View();
        }


        public FileContentResult ShowAvatar()
        {
            //On récupère le pseudo du l'utilisateur courant, il est unique donc OK
            var pseudo = UtilisateurService.GetUtilisateurCourantPseudo().Valeur;

            //On récupère le client correspondant
            var client = UtilisateurService.GetEntityByPseudo(pseudo);

            byte[] imageByte = client.Valeur.Avatar;
            string contentType = "image/jpeg";

            return File(imageByte, contentType);
        }

        [HttpPost]
        public ActionResult Pseudo(RegisterModel model)
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

    }
}
