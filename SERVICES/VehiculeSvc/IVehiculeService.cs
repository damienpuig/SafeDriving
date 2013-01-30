using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.VehiculeSvc
{
   public interface IVehiculeService
    {
       Resultat<IList<Vehicule>> GetVehicules();
       Resultat<Vehicule> GetVehiculeById(int id);
       Resultat<Vehicule> CreateVehiculeCommand(CreateVehiculeCommand command);
       Resultat<Vehicule> UpdateVehiculeCommand(UpdateVehiculeCommand command);

       Resultat<IList<Vehicule>> RemoveVehicules();
       Resultat<Vehicule> RemoveVehiculeById(int id);

    }
}
