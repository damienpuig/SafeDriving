using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.VehiculeSvc
{
   public class VehiculeService : IVehiculeService
    {

        public Resultat<IList<Vehicule>> GetVehicules()
        {
            return Resultat<IList<Vehicule>>.SafeExecute<VehiculeService>(
                result =>
                {

                });
        }

        public Resultat<Vehicule> GetVehiculeById(int id)
        {
            return Resultat<Vehicule>.SafeExecute<VehiculeService>(
      result =>
      {

      });
        }

        public Resultat<Vehicule> CreateVehiculeCommand(CreateVehiculeCommand command)
        {
            return Resultat<Vehicule>.SafeExecute<VehiculeService>(
      result =>
      {

      });
        }

        public Resultat<Vehicule> UpdateVehiculeCommand(UpdateVehiculeCommand command)
        {
            return Resultat<Vehicule>.SafeExecute<VehiculeService>(
     result =>
     {

     });
        }

        public Resultat<IList<Vehicule>> RemoveVehicules()
        {
            return Resultat<IList<Vehicule>>.SafeExecute<VehiculeService>(
     result =>
     {

     });
        }

        public Resultat<Vehicule> RemoveVehiculeById(int id)
        {
            return Resultat<Vehicule>.SafeExecute<VehiculeService>(
    result =>
    {

    });
        }
    }
}
