using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ForfaitReferenceSvc
{
   public class ForfaitReferenceService : IForfaitReferenceService
    {
        public SafeDrivingEntities context { get; set; }

       public ForfaitReferenceService()
       {
           context = SafeDrivingEntities.contexteDatas;
       }

        public Resultat<IList<ForfaitReference>> GetForfaitReferenceList()
        {
            return Resultat<IList<ForfaitReference>>.SafeExecute<ForfaitReferenceService>(
                result =>
                {

                });
        }

        public Resultat<ForfaitReference> GetForfaitReferenceById(int id)
        {
            return Resultat<ForfaitReference>.SafeExecute<ForfaitReferenceService>(
     result =>
     {

     });
        }

        public Resultat<ForfaitReference> CreateForfaitReference(CreateForfaitReferenceCommand command)
        {
            return Resultat<ForfaitReference>.SafeExecute<ForfaitReferenceService>(
result =>
{

});
        }

        public Resultat<ForfaitReference> UpdateForfaitReferenceReference(UpdateForfaitReferenceCommand command)
        {
            return Resultat<ForfaitReference>.SafeExecute<ForfaitReferenceService>(
result =>
{

});
        }

        public Resultat<ForfaitReference> RemoveForfaitReferenceById(int id)
        {
            return Resultat<ForfaitReference>.SafeExecute<ForfaitReferenceService>(
result =>
{

});
        }

        public Resultat<IList<ForfaitReference>> RemoveAllForfaitReferences()
        {
            return Resultat<IList<ForfaitReference>>.SafeExecute<ForfaitReferenceService>(
result =>
{

});
        }
    }
}
