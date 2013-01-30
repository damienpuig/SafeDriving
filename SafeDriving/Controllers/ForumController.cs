using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICES;
using SERVICES.ForumSvc;
using AutoMapper;
using POCO;
using SafeDriving.Models.CategorieViewModels;
using SafeDriving.Models.TopicViewModels;
using SERVICES.ImageSvc;
using SERVICES.UtilisateurSvc;

namespace SafeDriving.Controllers
{
    public class ForumController : Controller
    {
        public IForumService ForumService { get; set; }
        public IImageService ImageService { get; set; }
        public IUtilisateurService<Client> UtilisateurService { get; set; }

        public ForumController(IForumService forumService, IImageService imageService, IUtilisateurService<Client> utilisateurService)
        {
            ForumService = forumService;
            ImageService = imageService;
            UtilisateurService = utilisateurService;
        }

        public ActionResult Index()
        {
            //On récupère la liste des catégorie en base
            var resultat = ForumService.GetCategorieList();


            if (resultat.IsValid && resultat.Valeur.Count > 0)
            {
                var listeCategorieViewModel = Mapper.Map<IList<Categorie>, List<CategorieViewModel>>(resultat.Valeur);

                //On récupère les pseudo des créateurs
                IList<string> listeNomCreateur = new List<string>();
                foreach (var categorie in resultat.Valeur)
                {
                    var createurID = categorie.CreateurId;
                    var createur = ForumService.GetCreateurById(createurID);
                    listeNomCreateur.Add(createur.Valeur.Pseudo);
                }

                //On les affectent au VM
                for (int i = 0; i < listeCategorieViewModel.Count(); i++)
                {
                    listeCategorieViewModel[i].NomCreateur = listeNomCreateur[i];
                }

                return View(listeCategorieViewModel);
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

        //Ici on crée un nouveau topic
        [HttpPost]
        public ActionResult Index(IList<CategorieViewModel> categorieViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("Forum/Index");
            }
            else
            {
                //On récupère l'utilisateur courant
                var pseudo = UtilisateurService.GetUtilisateurCourantPseudo().Valeur;
                var utilisateurCourant = UtilisateurService.GetEntityByPseudo(pseudo);

                //On récupère la catégorie selectionné par l'utilisateur
                var categorie = ForumService.GetCategorieByNom(categorieViewModel[0].Nom);

                // On crée la commande
                // A l'ancienne cousin !
                CreateTopicCommand createTopicCommand = new CreateTopicCommand { CategorieId = categorie.Valeur.Id, Nom = categorieViewModel[0].TitreNewTopic, Contenu = categorieViewModel[0].MessageNewTopic, DateCreation=DateTime.Now, CreateurId=utilisateurCourant.Valeur.Id };
                var result = ForumService.CreateTopic(createTopicCommand);

                if (result.IsValid)
                {
                    return Redirect("Forum/Index");
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

        //Ici on crée une nouvelle catégorie
        //[HttpPost]
        //public ActionResult Categorie(IList<CategorieViewModel> categorieViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Redirect("Index");
        //    }
        //    else
        //    {
        //        //On récupère l'utilisateur courant
        //        var pseudo = UtilisateurService.GetUtilisateurCourantPseudo().Valeur;
        //        var utilisateurCourant = UtilisateurService.GetEntityByPseudo(pseudo);

        //        // On crée la commande
        //        // A l'ancienne cousin !
        //        var createCategorieCommand = new CreateCategorieCommand { CreateurId = utilisateurCourant.Valeur.Id, DateCreation = DateTime.Now, Nom = categorieViewModel[0].TitreNewCategorie };
        //        var result = ForumService.CreateCategorie(createCategorieCommand);

        //        if (result.IsValid)
        //        {
        //            return Redirect("Index");
        //        }
        //        else
        //        {
        //            //Pour les dev
        //            foreach (var erreur in result.Erreurs)
        //            {
        //                ModelState.AddModelError(erreur.ExecutionException.Source, erreur.ExecutionException);
        //            }

        //            //Pour les gens (affiche la page error.cshtml
        //            throw new Exception("Erreur inattendue dans l'application");
        //        }
        //    }
        //}

        public ActionResult Topic(int id)
        {
            //On récupère le topic
            var resultat = ForumService.GetTopicById(id);

            if (resultat.IsValid)
            {
                var createurId = resultat.Valeur.CreateurId;
                
                //On récupère le créateur du topic
                var createurResultat = ForumService.GetCreateurById(createurId);

                //Et les noms des créateurs des messages du topic
                //Ce qui va suivre.. bref ! :)
                var listeNomCreateur = new List<string>();
                var listeIdCreateur = new List<int>();

                foreach (var message in resultat.Valeur.Messages)
                {
                    var createurMessageId = message.CreateurId;
                    var createurMessageResultat = ForumService.GetCreateurById(createurMessageId);
                    listeNomCreateur.Add(createurMessageResultat.Valeur.Pseudo);
                    listeIdCreateur.Add(createurMessageId);
                }

                if (createurResultat.IsValid)
                {
                    var topicViewModel = Mapper.Map<Topic, TopicViewModel>(resultat.Valeur);
                  
                    var imageCreateur = ImageService.StreamToImage(createurResultat.Valeur.Avatar);

                    topicViewModel.NomCreateur = createurResultat.Valeur.Pseudo;
                    topicViewModel.IdCreateur = createurId;

                    for (int i = 0; i < topicViewModel.Messages.Count(); i++)
                    {
                        topicViewModel.Messages[i].NomCreateur = listeNomCreateur[i];
                        topicViewModel.Messages[i].IdCreateur = listeIdCreateur[i];
                    }

                    return View(topicViewModel);
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

        [HttpPost]
        public ActionResult Topic(TopicViewModel topicViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(topicViewModel.Id.ToString());
            }
            else
            {
                //On récupère l'utilisateur courant
                var pseudo = UtilisateurService.GetUtilisateurCourantPseudo().Valeur;
                var utilisateurCourant = UtilisateurService.GetEntityByPseudo(pseudo);

                // On crée la commande
                // A l'ancienne cousin !
                CreateMessageCommand createMessageCommand = new CreateMessageCommand { Contenu = topicViewModel.MessageNewTopic, TopicId = topicViewModel.Id, DateCreation = DateTime.Now, CreateurId = utilisateurCourant.Valeur.Id };
                
                var result = ForumService.CreateMessage(createMessageCommand);

                if (result.IsValid)
                {
                    return Redirect(topicViewModel.Id.ToString());//("Topic", new { id = topicViewModel.Id });
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

        public FileContentResult ShowAvatar(int id)
        {
            //On récupère le client correspondant
            var client = ForumService.GetCreateurById(id);

            byte[] imageByte = client.Valeur.Avatar;
            string contentType = "image/jpeg";

            return File(imageByte, contentType);
        }

        [HttpPost]
        public ActionResult EditPartial(TopicViewModel model)
        {
            return Json(new
            {
                Success = true
            });
        }



    }
}
