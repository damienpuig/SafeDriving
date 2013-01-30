using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ForfaitReferenceSvc
{
    public class UpdateForfaitReferenceCommand
    {
        public string Nom { get; set; }
        public IList<OffresReference> OffresReferences { get; set; }

    }
}
