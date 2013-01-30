using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POCO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SafeDriving.Models.PlanningViewModels
{
    public class PlanningViewModel
    {

        [Display(Name = "Liste des sessions disponibles :")]
        public IEnumerable<SelectListItem> ListSessions { get; set; }

        public Client Client { get; set; }


        public OffresReference SelectedOffre { get; set; }
        public TypeSession SelectedTypeSession { get; set; }
        public DateTime SelectedDate { get; set; }
        public Session SelectedSession { get; set; }
    }
}