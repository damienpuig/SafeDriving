using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES.RoleSvc;
using SERVICES.UtilisateurSvc;
using POCO;

namespace Formateur.Helpers
{
    public class AttributeHelpers
    {
        //Test d'autorisation
        public class CustomAutorizationAttribute : AuthorizeAttribute
        {

            public Privilege Role { get; set; }


            private IUtilisateurService<Employe> UtilisateurService
            {
                get;
                set;
            }

            public CustomAutorizationAttribute()
            {
                UtilisateurService = new EmployeService(new RoleService());
            }

            protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
            {
                var user = filterContext.HttpContext.User;
                if (user.Identity.IsAuthenticated)
                {
                    filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult(403);
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                if (httpContext == null)
                    throw new ArgumentNullException("httpContext");

                if (!httpContext.User.Identity.IsAuthenticated)
                    return false;

                var currentPseudo = UtilisateurService.GetUtilisateurCourantPseudo();
                var currentUser = UtilisateurService.GetEntityByPseudo(currentPseudo.Valeur);

                if (!currentPseudo.IsValid || !currentUser.IsValid)
                {
                    return false;
                }

                var role = UtilisateurService.GetRoleByEntityId(currentUser.Valeur.Id);

                //Ici on teste si l'id en base du privilege de l'utilisateur en cours est egal au privilege donnée par l'attribut.
                // On peut ici utiliser le flaguage d'enum.
                if (role.Valeur != Role)
                {
                    return false;
                }


                return true;
            }

        }
    }
}