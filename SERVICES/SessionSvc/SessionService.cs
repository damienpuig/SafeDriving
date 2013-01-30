using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using AutoMapper;

namespace SERVICES.SessionSvc
{
   public class SessionService : ISessionService
    {
       public SafeDrivingEntities context { get; set; }

       public SessionService()
        {
            context = SafeDrivingEntities.contexteDatas;
        }

       public Resultat<IList<Session>> GetSessionByFormateur(int formateurId)
       {
           return Resultat<IList<Session>>.SafeExecute<SessionService>(
               result =>
               {
                   var sessions = (from s in context.Sessions where s.EmployeId == formateurId select s).ToList();
                   result.Valeur = sessions;
               });
       }

       public Resultat<Session> GetSessionById(int Id)
       {
           return Resultat<Session>.SafeExecute<SessionService>(
             result =>
             {
                 var session = (from s in context.Sessions where s.Id == Id select s).First();
                 context.LoadProperty(session, o => o.Circuit);
                 context.LoadProperty(session, o => o.TypeSession);
                 result.Valeur = session;
             });
       }

       public Resultat<TypeSession> GetTypeSessionByName(string name)
       {
           return Resultat<TypeSession>.SafeExecute<SessionService>(
            result =>
            {
                var typeSession = (from s in context.TypeSessions where s.Nom == name select s).First();
                result.Valeur = typeSession;
            });
       }

       public Resultat<Circuit> GetCircuitByName(string name)
       {
           return Resultat<Circuit>.SafeExecute<SessionService>(
           result =>
           {
               var circuit = (from s in context.Circuits where s.Nom == name select s).First();
               result.Valeur = circuit;
           });
       }

       public Resultat<Dictionary<string, List<string>>> GetCircuitAndTypeSessionByDate(DateTime startDate)
       {
            return Resultat<Dictionary<string, List<string>>>.SafeExecute<SessionService>(
               result =>
               {
                   //On récupère les sessions commencant par la date recu
                   //On récupère tous les types de session et tous les circuit 
                   var sessions = (from s in context.Sessions where s.HeureDebut == startDate select s).ToList();
                   var circuits = (from c in context.Circuits select c).ToList();
                   var typeSessions = (from t in context.TypeSessions where t.Id < 6 select t).ToList();

                   foreach (var session in sessions)
                   {
                       if (circuits.Contains(session.Circuit))
                           circuits.Remove(session.Circuit);

                       if (typeSessions.Contains(session.TypeSession))
                           typeSessions.Remove(session.TypeSession);
                   }

                   //Une liste de liste
                   var listeCircuitAndTypeSession = new Dictionary<string,List<string>>();

                   var listeCircuitDispo = new List<string>();
                   var listeTypeSessionDispo = new List<string>();

                   foreach (var circuit in circuits)
                   {
                       listeCircuitDispo.Add(circuit.Nom);
                   }

                   foreach (var typeSession in typeSessions)
                   {
                       listeTypeSessionDispo.Add(typeSession.Nom);
                   }

                   listeCircuitAndTypeSession.Add("circuit", listeCircuitDispo);
                   listeCircuitAndTypeSession.Add("typesession", listeTypeSessionDispo);

                    result.Valeur = listeCircuitAndTypeSession;

               });
       }

       public Resultat<Session> CreateSession(CreateSessionCommand command)
       {
           return Resultat<Session>.SafeExecute<SessionService>(
                result =>
                {
                    var session = Mapper.Map<CreateSessionCommand, Session>(command);
                    context.Sessions.AddObject(session);
                    context.SaveChanges();
                });
       }

       public Resultat<Session> RemoveSession(string sessionName)
       {
           return Resultat<Session>.SafeExecute<SessionService>(
            result =>
            {
                var session = (from s in context.Sessions where s.Nom == sessionName select s).First();
                context.Sessions.DeleteObject(session);
                context.SaveChanges();
            });
       }




       /////////////////////////////////
       //Ne sert a rien pour l'instant
       ////////////////////////////////


        public Resultat<IList<Session>> GetSessions()
        {
            return Resultat<IList<Session>>.SafeExecute<SessionService>(
                result =>
                {

                });
        }

        

        

        public Resultat<Session> UpdateSession(UpdateSessionCommand command)
        {
            return Resultat<Session>.SafeExecute<SessionService>(
     result =>
     {

     });
        }

        public Resultat<IList<Session>> RemoveSessions()
        {
            return Resultat<IList<Session>>.SafeExecute<SessionService>(
     result =>
     {

     });
        }


        public Resultat<IList<Session>> GetSessionByType(TypeSession typeSession)
        {
            return Resultat<IList<Session>>.SafeExecute<SessionService>(
                result =>
                {

                });
        }
    }
}
