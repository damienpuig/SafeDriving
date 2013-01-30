using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES.ArticleSvc;
using AutoMapper;
using Formateur.Models.ArticleViewModels;
using POCO;
using SERVICES.RoleSvc;

namespace Formateur.Controllers
{
    public class ActuController : Controller
    {
        private IArticleService ArticleService { get; set; }

        public ActuController(IArticleService articleService)
        {
            ArticleService = articleService;
        }

        [Formateur.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Formateur)]
        public ActionResult Index()
        {
            //On récupère la liste des articles en base, 1 pour les actus
            var resultat = ArticleService.GetArticleByType(1);

            if (resultat.IsValid && resultat.Valeur.Count > 0)
            {
                var listeArticleViewModel = Mapper.Map<IList<Article>, List<ArticleViewModel>>(resultat.Valeur);
                return View(listeArticleViewModel);
            }
            else
            {
                //Pour les dev
                foreach (var erreur in resultat.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

        [Formateur.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Formateur)]
        public ActionResult Detail(string id)
        {
            int articleId = Convert.ToInt32(id);

            //On récupère l'article via son id
            var resultat = ArticleService.GetArticleById(articleId);

            if (resultat.IsValid)
            {
                var article = Mapper.Map<Article, ArticleViewModel>(resultat.Valeur);
                return View(article);
            }
            else
            {
                //Pour les dev
                foreach (var erreur in resultat.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

    }
}
