using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES
{
   public class Resultat<T>
    {

        public T Valeur { get; set; }
        public IList<Erreur> Erreurs { get; set; }
        public bool IsValid
        {
            get
            {
                if(Erreurs.Count > 0)
                {
                    return false; 
                }
                else
                {
                    return true;             
                }
            }
        }

        public Resultat()
        {
            Erreurs = new List<Erreur>();
        }

       public static Resultat<T> SafeExecute<U>(Action<Resultat<T>> ExecutionCode)
        {
            Resultat<T> resultat = new Resultat<T>();

            try
            {
                ExecutionCode(resultat);

            }
            catch (Exception exe)
            {
                resultat.Erreurs.Add(new Erreur() { ExecutionException = exe});
            }
           
            return resultat;
        }


    }
}
