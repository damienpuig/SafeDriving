using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Direction.AutoMapper
{
    public class DateTimeCustomResolver
    {
        public class DateTimeToStringResolver : ValueResolver<DateTime, string>
        {
            protected override string ResolveCore(DateTime date)
            {
                return date.ToString("dd-MM-yyyy") ;
            }
        }

        public class StringToDateTimeResolver : ValueResolver<string, DateTime>
        {
            protected override DateTime ResolveCore(string source)
            {
                DateTime date;
                var success = DateTime.TryParse(source, out date);
                if(success)
                {
                    return date; 
                }
                else
                {
                    return default(DateTime);
                }
            }
        }
    }
}