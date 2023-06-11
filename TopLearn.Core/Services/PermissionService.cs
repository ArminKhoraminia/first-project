using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Permission;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class PermissionService : IPermissionService
    {
        #region Create Context

        private TopLearnContext _context;
        public PermissionService(TopLearnContext context)
        {
            _context = context;
        }

        #endregion

        #region Role
        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public int UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return role.RoleId;
        }
        public bool DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }

        #endregion

        #region Permission

        public List<Permission> GetAllPermissions()
        {
            return _context.Permission.ToList();
        }

        public bool AddRolePermission(int RoleId, List<int> PermissionId)
        {
            foreach (var item in PermissionId)
            {
                _context.RolePermission.Add(new RolePermission
                {
                    RoleId = RoleId,
                    PermissionId = item
                });
            }
            _context.SaveChanges();
            return true;
        }

        public List<int> GetPermissionsByRoleId(int RoleId)
        {
            return _context.RolePermission.Where(r => r.RoleId == RoleId).Select(r => r.PermissionId).ToList();
        }

        public bool DeleteRolePermission(int RoleId)
        {
            _context.RolePermission.Where(r => r.RoleId == RoleId).ToList()
                .ForEach(r => _context.RolePermission.Remove(r));
            return true;
        }

        public bool UpdateRolePermission(int RoleId, List<int> PermissionId)
        {
            bool isSucces = false;
            isSucces = DeleteRolePermission(RoleId);
            if (isSucces)
                return AddRolePermission(RoleId, PermissionId);
            else
                return false;
        }

        #endregion

        #region Check Permission

        public bool UserHavePermission(int permissionId, string userName)
        {
            int UserId = new UserService(_context).GetIdByUserName(userName);

            List<int> UserRoles = _context.UserRoles.Where(r => r.UserId == UserId).Select(r => r.RoleId).ToList();
            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _context.RolePermission.Where(r=>r.PermissionId== permissionId)
                .Select(r => r.RoleId).ToList();

            return RolesPermission.Any(p => UserRoles.Contains(p));
        }

        #endregion

    }
}
