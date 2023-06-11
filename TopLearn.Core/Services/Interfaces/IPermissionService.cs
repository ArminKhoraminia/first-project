using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Permission;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Role

        int AddRole(Role role);
        Role GetRoleById(int roleId);
        int UpdateRole(Role role);
        bool DeleteRole(Role role);

        #endregion

        #region Permission

        List<Permission> GetAllPermissions();
        bool AddRolePermission(int RoleId , List<int> PermissionId);
        List<int> GetPermissionsByRoleId(int RoleId);
        bool DeleteRolePermission(int RoleId);
        bool UpdateRolePermission(int RoleId, List<int> PermissionId);

        #endregion

        #region Check Permission

        bool UserHavePermission(int permissionId, string userName);

        #endregion
    }
}
