using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES.RoleSvc;
using POCO;
using SERVICES.UtilisateurSvc;
using Formateur.Models;
using System.Web.Security;

namespace Direction.Controllers
{
    public class AccountController : Controller
    {
        public IRoleService RoleService { get; set; }
        public IUtilisateurService<Employe> UtilisateurService { get; set; }
        public IEmployeService EmployeService { get; set; }

        public AccountController(IEmployeService employeService, IRoleService roleService, IUtilisateurService<Employe> utilisateurService)
        {
            EmployeService = employeService;
            RoleService = roleService;
            UtilisateurService = utilisateurService;
        }
        
        public ActionResult Logon()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {

                if (!string.IsNullOrWhiteSpace(model.UserName) && !string.IsNullOrWhiteSpace(model.Password))
                {
                    var result = UtilisateurService.ValidateEntity(model.UserName, model.Password);

                    if (result.IsValid)
                    {
                        //On check le role, ici Admin
                        var role = UtilisateurService.GetRoleByEntityId(result.Valeur.Id);
                        if (role.Valeur == Privilege.Admin)
                        {
                            UtilisateurService.SignIn(result.Valeur, model.RememberMe);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["Message"] = "Vous n etes pas authorise a vous loguer sur ce site";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Vous n etes pas authorise a vous loguer sur ce site";
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

    }
}
