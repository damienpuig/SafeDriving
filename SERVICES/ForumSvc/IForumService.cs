using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ForumSvc
{
    public interface IForumService
    {
        /// <summary>
        /// Récupère la liste de tout les catégories
        /// </summary>
        /// <returns></returns>
        Resultat<IList<Categorie>> GetCategorieList();

        /// <summary>
        /// Récupère un Topic
        /// </summary>
        /// <param name="id">l'id du topic</param>
        /// <returns></returns>
        Resultat<Topic> GetTopicById(int id);

        /// <summary>
        /// Récupère un créateur
        /// </summary>
        /// <param name="id">l'id du créateur</param>
        /// <returns></returns>
        Resultat<Utilisateur> GetCreateurById(int id);

        /// <summary>
        /// Récupère une catégorie
        /// </summary>
        /// <param name="nom">le nom de la catégorie</param>
        /// <returns></returns>
        Resultat<Categorie> GetCategorieByNom(string nom);

        /// <summary>
        /// Créer un message pour le forum
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Resultat<Message> CreateMessage(CreateMessageCommand command);

        /// <summary>
        /// Créer un topic pour le forum
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Resultat<Topic> CreateTopic(CreateTopicCommand command);

        /// <summary>
        /// Créer une categorie pour le forum
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Resultat<Categorie> CreateCategorie(CreateCategorieCommand command);
    }
}
