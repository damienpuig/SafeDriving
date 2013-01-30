using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Direction.Models.PlanningViewModels
{
    public class PlanningViewModel
    {
        public Dictionary<int,string> ListeFormateur { get; set; }

        public int SelectedFormateur { get; set; }

        public SessionViewModel SelectedSessionViewModel { get; set; }
    }
}