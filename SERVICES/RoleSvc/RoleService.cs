using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.RoleSvc
{
    public class RoleService : IRoleService
    {
        public SafeDrivingEntities context { get; set; }

        public RoleService()
        {
            context = SafeDrivingEntities.contexteDatas;
        }
            
        public Resultat<Role> GetRoleByPrivilege(Privilege privilege)
        {
            return Resultat<Role>.SafeExecute<RoleService>(
                 result =>
                 {
                     int roleId = (int)privilege;

                     var role = context.Roles.Where(r => r.Id == roleId).First<Role>();
                     result.Valeur = role;
                 });
        }
    }
}
