using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.CircuitSvc
{
   public interface ICircuitService
    {

        Resultat<IList<Circuit>> GetCircuitList();
        Resultat<Circuit> GetCircuitById(int id);

        Resultat<Circuit> CreateCircuit(CreateCircuitCommand command);
        Resultat<Circuit> UpdateCircuit(UpdateCircuitCommand command);

        Resultat<Circuit> RemoveCircuitById(int id);
        Resultat<IList<Circuit>> RemoveAllCircuits();
    }
}
