using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ParcoursSvc
{
   public class ParcoursDto
    {
        public Dictionary<OffreReferenceDto, IList<SessionHistoriqueDto>> GraphDictionnary { get; set; }
    }
}
