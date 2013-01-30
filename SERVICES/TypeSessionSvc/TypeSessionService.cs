using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.TypeSessionSvc
{
   public class TypeSessionService : ITypeSessionService
    {
        public Resultat<TypeSession> GetTypeSessionById(int id)
        {
            return Resultat<TypeSession>.SafeExecute<TypeSessionService>(
                result =>
                {

                });
        }

        public Resultat<IList<TypeSession>> GetTypeSessions()
        {
            return Resultat<IList<TypeSession>>.SafeExecute<TypeSessionService>(
      result =>
      {

      });
        }

        public Resultat<TypeSession> CreateTypeSession(CreateTypeSessionCommand command)
        {
            return Resultat<TypeSession>.SafeExecute<TypeSessionService>(
     result =>
     {

     });
        }

        public Resultat<TypeSession> UpdateTypeSession(UpdateTypeSessionCommand command)
        {
            return Resultat<TypeSession>.SafeExecute<TypeSessionService>(
      result =>
      {

      });
        }

        public Resultat<TypeSession> RemoveTypeSessionById(int id)
        {
            return Resultat<TypeSession>.SafeExecute<TypeSessionService>(
     result =>
     {

     });
        }

        public Resultat<IList<TypeSession>> RemoveTypeSessions()
        {
            return Resultat<IList<TypeSession>>.SafeExecute<TypeSessionService>(
     result =>
     {

     });
        }
    }
}
