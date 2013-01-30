using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.DateSvc
{
    public class DateService : IDateService
    {
        public SafeDrivingEntities context { get; set; }
        public DateService()
        {
            context = SafeDrivingEntities.contexteDatas;
        }

        public Resultat<IList<Date>> GetDateList()
        {
            return Resultat<IList<Date>>.SafeExecute<DateService>(
                result =>
                {

                });
        }

        public Resultat<Date> GetDateById(int id)
        {
            return Resultat<Date>.SafeExecute<DateService>(
              result =>
             {

             });
        }

        public Resultat<Date> CreateDate(CreateDateCommand command)
        {
            return Resultat<Date>.SafeExecute<DateService>(
            result =>
            {
            
            });
        }

        public Resultat<Date> UpdateDate(UpdateDateCommand command)
        {
            return Resultat<Date>.SafeExecute<DateService>(
result =>
{

});
        }

        public Resultat<Date> RemoveDateById(int id)
        {
            return Resultat<Date>.SafeExecute<DateService>(
result =>
{

});
        }

        public Resultat<IList<Date>> RemoveAllDates()
        {
            return Resultat<IList<Date>>.SafeExecute<DateService>(
result =>
{

});
        }
    }
}
