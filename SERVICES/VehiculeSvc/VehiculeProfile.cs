using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using POCO;

namespace SERVICES.VehiculeSvc
{
    public class VehiculeProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "VehiculeProfile";
            }
        }

        protected override void Configure()
        {
            CreateMap<CreateVehiculeCommand, Vehicule>()
                .ForMember(Vehicule => Vehicule.Id, o => o.Ignore());

            CreateMap<UpdateVehiculeCommand, Vehicule>()
               .ForMember(Vehicule => Vehicule.Id, o => o.Ignore());
        }
    }
}

