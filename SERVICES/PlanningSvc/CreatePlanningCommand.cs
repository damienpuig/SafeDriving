using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.PlanningSvc
{
   public class CreatePlanningCommand
    {
        public int SelectedOffreId { get; set; }
        public int SelectedSessionId { get; set; }
        public int ClientId { get; set; }
    }
}
