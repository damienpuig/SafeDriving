using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ForumSvc
{
    public class ForumService : IForumService
    {
        public SafeDrivingEntities context { get; set; }

        public ForumService()
        {
            context = SafeDrivingEntities.contexteDatas;

        }

        public Resultat<IList<Categorie>> GetCategorieList()
        {
            return Resultat<IList<Categorie>>.SafeExecute<ForumService>(
                result =>
                {
                    var categorie = context.Categories.ToList();
                    foreach (var item in categorie)
                    {
                        context.LoadProperty(item,o=>o.Topics);
                    }
                    result.Valeur = categorie;
                });
        }

        public Resultat<Topic> GetTopicById(int id)
        {
            return Resultat<Topic>.SafeExecute<ForumService>(
               result =>
               {
                   var topic = (from t in context.Topics where t.Id.Equals(id) select t).FirstOrDefault();
                   context.LoadProperty(topic, o => o.Messages);
                   result.Valeur = topic ;
               });
        }

        public Resultat<Utilisateur> GetCreateurById(int id)
        {
            return Resultat<Utilisateur>.SafeExecute<ForumService>(
               result =>
               {
                   var createur = (from c in context.Utilisateurs where c.Id.Equals(id) select c).FirstOrDefault();
                   result.Valeur = createur;
               });
        }

        public Resultat<Categorie> GetCategorieByNom(string nom)
        {
            return Resultat<Categorie>.SafeExecute<ForumService>(
               result =>
               {
                   var categorie = (from c in context.Categories where c.Nom.Equals(nom) select c).FirstOrDefault();
                   result.Valeur = categorie;
               });
        }

        public Resultat<Message> CreateMessage(CreateMessageCommand command)
        {
            return Resultat<Message>.SafeExecute<ForumService>(
                 result =>
                 {
                     var message = new Message { Contenu = command.Contenu, CreateurId = command.CreateurId, DateCreation = command.DateCreation, TopicId = command.TopicId };
                     context.Messages.AddObject(message);
                     context.SaveChanges();
                 });
        }

        public Resultat<Topic> CreateTopic(CreateTopicCommand command)
        {
            return Resultat<Topic>.SafeExecute<ForumService>(
                 result =>
                 {
                     var topic = new Topic { CategorieId = command.CategorieId, Contenu = command.Contenu, CreateurId = command.CreateurId, DateCreation = command.DateCreation, Nom = command.Nom };
                     context.Topics.AddObject(topic);
                     context.SaveChanges();
                 });
        }

        public Resultat<Categorie> CreateCategorie(CreateCategorieCommand command)
        {
            return Resultat<Categorie>.SafeExecute<ForumService>(
                 result =>
                 {
                     var categorie = new Categorie { Nom = command.Nom, CreateurId = command.CreateurId, DateCreation = command.DateCreation };
                     context.Categories.AddObject(categorie);
                     context.SaveChanges();
                 });
        }
        
    }
}
