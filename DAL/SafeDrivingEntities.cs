using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Objects;
using System.Configuration;

namespace POCO
{
    public partial class SafeDrivingEntities
    {
        #region Gestion du context
        public static SafeDrivingEntities contexteDatas
        {
            get
            {
                SafeDrivingEntities c = null;

                if (HttpContext.Current != null)
                {

                    if (ListContextData.ContainsKey(HttpContext.Current.User.Identity.Name))
                    {
                        c = ListContextData[HttpContext.Current.User.Identity.Name];
                    }
                    else
                    {
                        c = SafeDrivingEntities.CreateContextData();
                        ListContextData.Add(HttpContext.Current.User.Identity.Name, c);
                    }

                }

                return c;
            }
        }

        private static Dictionary<string, SafeDrivingEntities> _listcontextData;
        public static Dictionary<string, SafeDrivingEntities> ListContextData
        {
            get { return SafeDrivingEntities._listcontextData; }
            set { SafeDrivingEntities._listcontextData = value; }
        }


        static SafeDrivingEntities()
        {
            ListContextData = new Dictionary<string, SafeDrivingEntities>();
        }

        private static SafeDrivingEntities CreateContextData()
        {
            SafeDrivingEntities ocontextdata = new SafeDrivingEntities(ConfigurationManager.ConnectionStrings["SafeDrivingEntities"].ConnectionString);
            return ocontextdata;
        }

        public static void RemoveContextData(string aIdContextData)
        {
            if (ListContextData.ContainsKey(aIdContextData))
            {
                ListContextData.Remove(aIdContextData);
            }
        }

        public static void SaveContextData()
        {
            SafeDrivingEntities.contexteDatas.SaveChanges();
        }

        #endregion
    }
}
