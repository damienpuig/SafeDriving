using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.DateSvc
{
   public interface IDateService
    {
        Resultat<IList<Date>> GetDateList();
        Resultat<Date> GetDateById(int id);

        Resultat<Date> CreateDate(CreateDateCommand command);
        Resultat<Date> UpdateDate(UpdateDateCommand command);

        Resultat<Date> RemoveDateById(int id);
        Resultat<IList<Date>> RemoveAllDates();
    }
}
