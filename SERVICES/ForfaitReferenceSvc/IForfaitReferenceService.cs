using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ForfaitReferenceSvc
{
   public interface IForfaitReferenceService
    {
        Resultat<IList<ForfaitReference>> GetForfaitReferenceList();
        Resultat<ForfaitReference> GetForfaitReferenceById(int id);

        Resultat<ForfaitReference> CreateForfaitReference(CreateForfaitReferenceCommand command);
        Resultat<ForfaitReference> UpdateForfaitReferenceReference(UpdateForfaitReferenceCommand command);

        Resultat<ForfaitReference> RemoveForfaitReferenceById(int id);
        Resultat<IList<ForfaitReference>> RemoveAllForfaitReferences();
    }
}
