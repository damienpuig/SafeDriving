using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Direction.Models.PlanningViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }

        public string TypeSession { get; set; }

        public string Circuit { get; set; }

        public string NbParticipant { get; set; }
    }
}
