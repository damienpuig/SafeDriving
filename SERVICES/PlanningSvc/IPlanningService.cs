using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using SERVICES.RoleSvc;

namespace SERVICES.PlanningSvc
{
   public interface IPlanningService
    {
       Resultat<IList<OffresReference>> GetIListOffreByClient(Client client);

       Resultat<IList<TypeSession>> GetListTypeSessionByOffre(POCO.Client client, int offreId);

       Resultat<IList<Session>> GetListFreesessionByTypeSessionAndDate(int typeSessionId, DateTime date);

       Resultat<bool> CreatePlanningInscription(CreatePlanningCommand command);

       //Coté formateur
       Resultat<Dictionary<int, string>> GetAllFormateur(Privilege role);
       
    }
}
