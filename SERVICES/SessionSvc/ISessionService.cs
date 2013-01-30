using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using SERVICES.UtilisateurSvc;

namespace SERVICES.SessionSvc
{
   public interface ISessionService
    {
       Resultat<IList<Session>> GetSessionByFormateur(int formateurId);

       Resultat<IList<Session>> GetSessions();
       Resultat<Session> GetSessionById(int Id);
       Resultat<TypeSession> GetTypeSessionByName(string name);
       Resultat<Circuit> GetCircuitByName(string name);
       Resultat<IList<Session>> GetSessionByType(TypeSession typeSession);
       Resultat<Session> CreateSession(CreateSessionCommand command);
       Resultat<Session> UpdateSession(UpdateSessionCommand command);
       Resultat<Session> RemoveSession(string sessionName);
       Resultat<IList<Session>> RemoveSessions();
       Resultat<Dictionary<string, List<string>>> GetCircuitAndTypeSessionByDate(DateTime startDate);
    }
}
