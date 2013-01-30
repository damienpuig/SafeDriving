using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ArticleSvc
{
   public interface IArticleService
    {
        Resultat<IList<Article>> GetArticleByType(int idType);
        Resultat<Article> GetArticleById(int id);
        Resultat<Dictionary<int, string>> GetArticleCategorie();
        Resultat<Article> CreateArticle(CreateArticleCommand command);
        Resultat<Article> UpdateArticle(UpdateArticleCommand command);
        Resultat<Article> RemoveArticleById(int id);
        Resultat<IList<Article>> RemoveAllArticles();
    }
}
