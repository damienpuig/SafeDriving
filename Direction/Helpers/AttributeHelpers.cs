using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;
using SERVICES.UtilisateurSvc;
using POCO;
using SERVICES.RoleSvc;
using System.Text.RegularExpressions;

namespace Direction.Helpers
{
    public class AttributeHelpers
    {

        //Test Password
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidatePasswordLengthAttribute : ValidationAttribute, IClientValidatable
        {
            private const string _defaultErrorMessage = "* Le mot de passe doit comporter {1} caractères minimum.";
            private readonly int _minCharacters = 6;

            public ValidatePasswordLengthAttribute()
                : base(_defaultErrorMessage)
            {
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                    name, _minCharacters);
            }

            public override bool IsValid(object value)
            {
                string valueAsString = value as string;
                return (valueAsString != null && valueAsString.Length >= _minCharacters);
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                return new[]{
                new ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()), _minCharacters, int.MaxValue)
            };
            }
        }


        //Test Pseudo
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidatePseudoIsUniqueAttribute : ValidationAttribute, IClientValidatable
        {
            private const string _PseudoErrorMessage = "* Ce pseudo est deja pris";
            public IUtilisateurService<Client> UtilisateurService { get; set; }

            public ValidatePseudoIsUniqueAttribute()
                : base(_PseudoErrorMessage)
            {
                UtilisateurService = new ClientService(new RoleService());
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                    name);
            }

            public override bool IsValid(object value)
            {
                var pseudo = value.ToString();
                return (UtilisateurService.IsPseudoUnique(pseudo).Valeur);
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                var test = metadata.ToString();
                var unique = UtilisateurService.IsPseudoUnique(test);
                return new[]{
                new ModelClientValidationEqualToRule(_PseudoErrorMessage,unique.Valeur)
            };
            }
        }


        //Test de Mail 
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidateMailAttribute : ValidationAttribute, IClientValidatable
        {
            private const string _defaultErrorMessage = "* Le mail doit etre de forme default@servermail.com .";
            Regex mailRegex = new Regex("([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})", RegexOptions.IgnoreCase);

            public ValidateMailAttribute()
                : base(_defaultErrorMessage)
            {
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                    name, _defaultErrorMessage);
            }

            public override bool IsValid(object value)
            {
                if (value != null)
                {
                    if (mailRegex.Match(value.ToString()).Success)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                return new[]{
                new ModelClientValidationRegexRule(_defaultErrorMessage, "([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})" )};
            }
        }


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