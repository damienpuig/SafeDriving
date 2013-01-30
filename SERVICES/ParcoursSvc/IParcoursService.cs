using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ParcoursSvc
{
   public interface IParcoursService
    {
       Resultat<ParcoursDto> GetParcoursSessionByClient(Client client);
    }
}
