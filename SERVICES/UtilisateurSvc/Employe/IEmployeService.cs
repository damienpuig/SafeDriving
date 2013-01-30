using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.UtilisateurSvc
{
    public interface IEmployeService
    {
        Resultat<POCO.Employe> CreateEmploye(CreateClientCommand command);
        Resultat<POCO.Employe> UpdateEmploye(UpdateClientCommand command);

        Resultat<IList<ClientNoteDto>> GetClientsNoteByFormateur(Employe employe);
        Resultat<ClientNoteDto> GetHistoriqueSession(Employe employe, int id);
        Resultat<bool> AddNoteToClientHistoricalSession(CreateClientNoteCommand command);
    }
}
