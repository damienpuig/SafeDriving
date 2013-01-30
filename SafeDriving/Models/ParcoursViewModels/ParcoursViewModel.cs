using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POCO;
using System.ComponentModel.DataAnnotations;
using SERVICES.ParcoursSvc;

namespace SafeDriving.Models.ParcoursViewModels
{
    public class ParcoursViewModel
    {
        public Dictionary<OffreReferenceDto, IList<SessionHistoriqueDto>> GraphDictionnary { get; set; }
    }
}