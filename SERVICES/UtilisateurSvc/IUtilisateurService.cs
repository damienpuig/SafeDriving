using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using SERVICES.RoleSvc;

namespace SERVICES.UtilisateurSvc
{
   public interface IUtilisateurService<T>
    {
       Resultat<IList<T>> GetEntities();

       Resultat<IList<T>> GetEntitiesByRole(Privilege role);

       Resultat<Privilege> GetRoleByEntityId(int id);

       Resultat<T> GetEntityById(int id);

       Resultat<T> RemoveEntityById(int id);

       Resultat<T> ValidateEntity(string login, string password);

       Resultat<T> SignIn(T entityToSignIn, bool isCookiePersistent);

       Resultat<string> GetUtilisateurCourantPseudo();

       Resultat<T> GetEntityByPseudo(string pseudo);

       Resultat<T> RemoveEntities();

       Resultat<bool> IsPseudoUnique(string pseudo); 

    }
}
