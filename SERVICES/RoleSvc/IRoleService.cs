using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.RoleSvc
{
    public interface IRoleService
    {
        Resultat<Role> GetRoleByPrivilege(Privilege privilege);

    }

    // Correspond aux privilege en base
    // NE PAS MODIFIER
    public enum Privilege
    {
        Admin = 1,
        Formateur = 2,
        Client = 3,
        Forum = 4
    }
}
