using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES.ArticleSvc;
using Direction.Models.ArticleViewModels;
using AutoMapper;
using SERVICES.UtilisateurSvc;
using POCO;
using SERVICES.RoleSvc;

namespace Direction.Controllers
{
    public class ArticlesController : Controller
    {
        public IArticleService ArticlService { get; set; }
        public IUtilisateurService<Client> UtilisateurService { get; set; }

        public ArticlesController(IArticleService articleService, IUtilisateurService<Client> utilisateurService)
        {
            ArticlService = articleService;
            UtilisateurService = utilisateurService;
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        public ActionResult Index()
        {
            //On récupère les catégorie d'article
            var categories = ArticlService.GetArticleCategorie();
            if (categories.IsValid)
            {
                var articleViewModel = new ArticleViewModel();
                articleViewModel.ListeCategorie = categories.Valeur;
                return View(articleViewModel);
            }
            else
            {
                //Pour les dev
                foreach (var erreur in categories.Erreurs)
                {
                    ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
                }

                //Pour les gens (affiche la page error.cshtml
                throw new Exception("Erreur inattendue dans l'application");
            }
        }

        [Direction.Helpers.AttributeHelpers.CustomAutorization(Role = Privilege.Admin)]
        [HttpPost]
        public ActionResult Index(ArticleViewModel articleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //on récupère l'utilisateurCourant
                var pseudoUtilisateurCourant = UtilisateurService.GetUtilisateurCourantPseudo();
                var utilisateurCourant = UtilisateurService.GetEntityByPseudo(pseudoUtilisateurCourant.Valeur);

                var createArticleCommand = new CreateArticleCommand { Contenu = articleViewModel.Contenu, Date = DateTime.Now, Titre = articleViewModel.Titre, TypeArticleId = articleViewModel.TypeArticleId, EmployeId = utilisateurCourant.Valeur.Id };
                var result = ArticlService.CreateArticle(createArticleCommand);
                if (result.IsValid)
                {
                    TempData["Message"] = "L article a ete cree avec succes !";
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
        }

    }
}
