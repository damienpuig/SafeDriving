using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using SERVICES.RoleSvc;

namespace SERVICES.UtilisateurSvc
{
   public interface IClientService
    {
       Resultat<IList<Client>> GetClientsByBirthDate(DateTime BirthDate);

       Resultat<Client> CreateClient(CreateClientCommand createClientCommand);

       Resultat<Client> UpdateEntity(UpdateClientCommand updateClientCommand);

       Resultat<bool> MigrateToCompteClient(Client clientToMigrate);

    }
}
