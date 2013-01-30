using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace SafeDriving.AutoMapper
{
    public class UpdatePasswordCustomResolver
    {
        public class UpdatePasswordResolver : ValueResolver<string, string>
        {
            protected override string ResolveCore(string password)
            {
                if (password != null && password.Length > 5)
                {
                    return password;
                }
                else
                {
                    return password = "INCHANGE";
                }
            }
        }
    }
}