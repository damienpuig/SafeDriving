using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ExamenSvc
{
    public interface IExamenService
    {
        IList<ClientDto> GetMoyenneForClientDto(IList<ClientDto> listeClientDto);

        IList<ClientDto> GetClientByHistorique(IList<Utilisateurs_ClientSessions> listeClientSession);

        IList<Utilisateurs_ClientSessions> GetUtilisateurs_ClientSessionsBySessionId(IList<Session> listeSession);

        IList<Session> GetSessionByTypeSessionId(int typeSessionId);
    }
}
