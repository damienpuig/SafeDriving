using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.CircuitSvc
{
    public class CircuitService : ICircuitService
    {

        public SafeDrivingEntities context { get; set; }

        public CircuitService()
        {
            context = SafeDrivingEntities.contexteDatas;
        }

        public Resultat<IList<Circuit>> GetCircuitList()
        {
            return Resultat<IList<Circuit>>.SafeExecute<CircuitService>(
                 result =>
                 {

                 });
        }

        public Resultat<Circuit> GetCircuitById(int id)
        {
            return Resultat<Circuit>.SafeExecute<CircuitService>(
      result =>
      {

      });
        }

        public Resultat<Circuit> CreateCircuit(CreateCircuitCommand command)
        {
            return Resultat<Circuit>.SafeExecute<CircuitService>(
      result =>
      {

      });
        }

        public Resultat<Circuit> UpdateCircuit(UpdateCircuitCommand command)
        {
            return Resultat<Circuit>.SafeExecute<CircuitService>(
      result =>
      {

      });
        }

        public Resultat<Circuit> RemoveCircuitById(int id)
        {
            return Resultat<Circuit>.SafeExecute<CircuitService>(
      result =>
      {

      });
        }

        public Resultat<IList<Circuit>> RemoveAllCircuits()
        {
            return Resultat<IList<Circuit>>.SafeExecute<CircuitService>(
       result =>
       {

       });
        }
    }
}
