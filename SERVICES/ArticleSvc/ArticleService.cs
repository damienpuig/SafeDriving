using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ArticleSvc
{
    public class ArticleService : IArticleService
    {
        public SafeDrivingEntities context { get; set; }

        public ArticleService()
        {
            context = SafeDrivingEntities.contexteDatas;
        }

        public Resultat<IList<Article>> GetArticleByType(int idType)
        {
            return Resultat<IList<Article>>.SafeExecute<ArticleService>(
                result =>
                {
                    var articles = (from a in context.Articles where a.TypeArticleId == idType select a).ToList();
                    result.Valeur = articles;
                });
        }

        public Resultat<Dictionary<int, string>> GetArticleCategorie()
        {
            return Resultat<Dictionary<int, string>>.SafeExecute<ArticleService>(
                 result =>
                 {
                     Dictionary<int, string> Types = new Dictionary<int, string>();

                     var typeArticle = context.TypeArticles.ToList();

                     foreach (var c in typeArticle)
                     {
                         Types.Add(c.Id, c.Nom);
                     }

                     result.Valeur = Types;
                 });
        }


        public Resultat<Article> GetArticleById(int id)
        {
            return Resultat<Article>.SafeExecute<ArticleService>(
                 result =>
                 {
                     var article = (from a in context.Articles where a.Id.Equals(id) select a).FirstOrDefault();
                     result.Valeur = article;
                 });
        }

        public Resultat<Article> CreateArticle(CreateArticleCommand command)
        {
            return Resultat<Article>.SafeExecute<ArticleService>(
                 result =>
                 {
                     var article = new Article { TypeArticleId=command.TypeArticleId, Contenu=command.Contenu, Date=DateTime.Now, EmployeId=command.EmployeId, Titre=command.Titre};
                     context.Articles.AddObject(article);
                     context.SaveChanges();
                 });
        }

        public Resultat<Article> UpdateArticle(UpdateArticleCommand command)
        {
            return Resultat<Article>.SafeExecute<ArticleService>(
             result =>
             {

             });
        }

        public Resultat<Article> RemoveArticleById(int id)
        {
            return Resultat<Article>.SafeExecute<ArticleService>(
             result =>
             {

             });
        }

        public Resultat<IList<Article>> RemoveAllArticles()
        {
            return Resultat<IList<Article>>.SafeExecute<ArticleService>(
            result =>
            {

            });
        }
    }
}
