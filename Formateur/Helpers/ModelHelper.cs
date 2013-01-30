using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Linq.Expressions;

namespace Formateur.Helpers
{
    public static class ModelHelper
    {
        public static object GetPropertyValue(string propertyParameterName, object o)
        {
            object obj = null;

            if (string.IsNullOrEmpty(propertyParameterName) == false &&
                o != null)
            {
                PropertyInfo p = o.GetType().GetProperty(propertyParameterName);

                if (p != null)
                    obj = p.GetValue(o, null);
            }
            return obj;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> exp)
        {
            return (((MemberExpression)(exp.Body)).Member).Name;
        }
    }
}