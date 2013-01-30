using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.TypeSessionSvc
{
   public interface ITypeSessionService
    {
       Resultat<TypeSession> GetTypeSessionById(int id);
       Resultat<IList<TypeSession>> GetTypeSessions();
       Resultat<TypeSession> CreateTypeSession(CreateTypeSessionCommand command);
       Resultat<TypeSession> UpdateTypeSession(UpdateTypeSessionCommand command);
       Resultat<TypeSession> RemoveTypeSessionById(int id);
       Resultat<IList<TypeSession>> RemoveTypeSessions();
    }
}
